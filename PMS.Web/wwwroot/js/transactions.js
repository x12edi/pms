$(document).ready(function () {
    let dataTable = null;

    function initTable(holdingId) {
        if (dataTable) {
            dataTable.destroy();
        }

        dataTable = $('#transactionTable').DataTable({
            processing: true,
            serverSide: true,
            ajax: {
                url: `/api/Transaction/holding/${holdingId}`,
                type: 'GET',
                data: function (d) {
                    return {
                        draw: d.draw,
                        start: d.start,
                        length: d.length,
                        searchValue: d.search.value,
                        orderColumn: d.order[0]?.column,
                        orderDir: d.order[0]?.dir
                    };
                },
                error: function (xhr) {
                    showModalMessage('Error', 'Failed to load transactions: ' + (xhr.responseText || 'Unknown error'), 'error');
                }
            },
            columns: [
                { data: 'id', className: 'dt-center' },
                { data: 'type', className: 'dt-center' },
                { data: 'quantity', className: 'dt-center' },
                {
                    data: 'price',
                    className: 'dt-center',
                    render: function (data) {
                        return `$${parseFloat(data).toFixed(2)}`;
                    }
                },
                {
                    data: 'amount',
                    className: 'dt-center',
                    render: function (data) {
                        return `$${parseFloat(data).toFixed(2)}`;
                    }
                },
                {
                    data: 'date',
                    className: 'dt-center',
                    render: function (data) {
                        return new Date(data).toLocaleDateString();
                    }
                },
                {
                    data: 'fees',
                    className: 'dt-center',
                    render: function (data) {
                        return `$${parseFloat(data).toFixed(2)}`;
                    }
                },
                {
                    data: null,
                    className: 'dt-center actions',
                    render: function (data) {
                        return `
                            <button class="btn btn-sm btn-primary btn-edit" data-id="${data.id}">
                                <i class="bi bi-pencil"></i> Edit
                            </button>
                            <button class="btn btn-sm btn-danger btn-delete" data-id="${data.id}">
                                <i class="bi bi-trash"></i> Delete
                            </button>
                        `;
                    },
                    orderable: false
                }
            ],
            pageLength: 10,
            responsive: true,
            language: {
                search: 'Filter by Type:',
                searchPlaceholder: 'e.g., Buy',
                processing: 'Loading transactions...'
            },
            aria: {
                sortAscending: ': activate to sort column ascending',
                sortDescending: ': activate to sort column descending'
            }
        });

        $('#transactionTable tbody').on('click', '.btn-edit', function () {
            let id = $(this).data('id');
            openEditModal(id);
        });

        $('#transactionTable tbody').on('click', '.btn-delete', function () {
            let id = $(this).data('id');
            if (confirm('Are you sure you want to delete this transaction?')) {
                deleteTransaction(id);
            }
        });
    }

    function showModalMessage(title, message, type) {
        const messageDiv = $('#modalMessage');
        messageDiv.text(message).removeClass('error success').addClass(type);
        setTimeout(() => messageDiv.text(''), 5000);
    }

    function resetAddModal() {
        $('#transactionModalLabel').text('Add Transaction');
        $('#transactionForm')[0].reset();
        $('#transactionId').val('');
        $('#formHoldingId').val($('#holdingId').val());
        $('#modalMessage').text('');
    }

    function openEditModal(id) {
        $.get(`/api/Transaction/${id}`).done(function (transaction) {
            $('#transactionModalLabel').text('Edit Transaction');
            $('#transactionId').val(transaction.id);
            $('#formHoldingId').val(transaction.holdingId);
            $('#type').val(transaction.type);
            $('#quantity').val(transaction.quantity);
            $('#price').val(transaction.price);
            $('#amount').val(transaction.amount);
            $('#fees').val(transaction.fees);
            $('#modalMessage').text('');
            $('#transactionModal').modal('show');
        }).fail(function (xhr) {
            showModalMessage('Error', 'Failed to load transaction: ' + (xhr.responseText || 'Unknown error'), 'error');
        });
    }

    function deleteTransaction(id) {
        $.ajax({
            url: `/api/Transaction/${id}`,
            type: 'DELETE',
            success: function () {
                dataTable.ajax.reload();
                showModalMessage('Success', 'Transaction deleted successfully.', 'success');
            },
            error: function (xhr) {
                showModalMessage('Error', 'Failed to delete transaction: ' + (xhr.responseText || 'Unknown error'), 'error');
            }
        });
    }

    $('#transactionForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#transactionId').val();
        const transaction = {
            holdingId: parseInt($('#formHoldingId').val()),
            type: $('#type').val(),
            quantity: parseFloat($('#quantity').val()),
            price: parseFloat($('#price').val()),
            amount: parseFloat($('#amount').val()),
            fees: parseFloat($('#fees').val())
        };

        if (isNaN(transaction.holdingId) || isNaN(transaction.quantity) || isNaN(transaction.price) || isNaN(transaction.amount) || isNaN(transaction.fees)) {
            showModalMessage('Error', 'Please fill all fields with valid numbers.', 'error');
            return;
        }

        const method = id ? 'PUT' : 'POST';
        const url = id ? `/api/Transaction/${id}` : '/api/Transaction';

        $.ajax({
            url: url,
            type: method,
            contentType: 'application/json',
            data: JSON.stringify(transaction),
            success: function () {
                $('#transactionModal').modal('hide');
                dataTable.ajax.reload();
                showModalMessage('Success', id ? 'Transaction updated successfully.' : 'Transaction created successfully.', 'success');
            },
            error: function (xhr) {
                showModalMessage('Error', 'Failed to save transaction: ' + (xhr.responseText || 'Unknown error'), 'error');
            }
        });
    });

    $('#addTransactionBtn').on('click', resetAddModal);

    $('#loadTransactionsBtn').on('click', function () {
        const holdingId = parseInt($('#holdingId').val());
        if (!holdingId || isNaN(holdingId)) {
            showModalMessage('Error', 'Please enter a valid Holding ID.', 'error');
            return;
        }
        initTable(holdingId);
    });

    initTable(parseInt($('#holdingId').val()) || 1);
});
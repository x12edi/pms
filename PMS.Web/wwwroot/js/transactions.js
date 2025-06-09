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
                    showMessage('Error', 'Failed to load transactions: ' + (xhr.responseText || 'Unknown error'), 'error', false);
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
                            <button class="btn btn-sm btn-outline-primary btn-edit" data-id="${data.id}" title="Edit" data-bs-toggle="tooltip">
                                <i class="bi bi-pencil"></i>
                            </button>
                            <button class="btn btn-sm btn-outline-danger btn-delete" data-id="${data.id}" title="Delete" data-bs-toggle="tooltip">
                                <i class="bi bi-trash"></i>
                            </button>
                        `;
                    },
                    orderable: false,
                    width: '80px'
                }
            ],
            pageLength: 10,
            responsive: true,
            language: {
                search: 'Filter by Type:',
            },
            aria: {
                sortAscending: ': activate to sort column ascending',
                sortDescending: ': activate to sort column descending',
            },
            drawCallback: function () {
                $('[data-bs-toggle="tooltip"]').tooltip();
            }
        });

        $('#transactionTable tbody').on('click', 'tr', function (e) {
            if (!$(e.target).closest('.btn-edit, .btn-delete').length) {
                let id = dataTable.row(this).data().id;
                openEditModal(id);
            }
        });

        $('#transactionTable tbody').on('click', '.btn-edit', function (e) {
            e.stopPropagation();
            let id = $(this).data('id');
            openEditModal(id);
        });

        $('#transactionTable tbody').on('click', '.btn-delete', function (e) {
            e.stopPropagation();
            let id = $(this).data('id');
            if (confirm('Are you sure you want to delete this transaction?')) {
                deleteTransaction(id);
            }
        });
    }

    function showMessage(title, message, type, inModal = true) {
        const messageDiv = inModal ? $('#modalMessage') : $('#alertMessage');
        messageDiv.text(message)
            .removeClass('error success alert-danger alert-success d-none')
            .addClass(inModal ? type : `alert alert-${type === 'error' ? 'danger' : 'success'}`);
        if (!inModal) {
            messageDiv.removeClass('d-none');
        }
        setTimeout(() => {
            messageDiv.text('');
            if (!inModal) {
                messageDiv.addClass('d-none');
            }
        }, 5000);
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
            showMessage('Error', 'Failed to load transaction: ' + (xhr.responseText || 'Unknown error'), 'error', false);
        });
    }

    function deleteTransaction(id) {
        $.ajax({
            url: `/api/Transaction/${id}`,
            type: 'DELETE',
            success: function () {
                dataTable.ajax.reload();
                showMessage('Success', 'Transaction deleted successfully.', 'success', false);
            },
            error: function (xhr) {
                showMessage('Error', 'Failed to delete transaction: ' + (xhr.responseText || 'Unknown error'), 'error', false);
            }
        });
    }

    function loadPortfolios() {
        console.log('Portfolios start:');
        $.get('/api/Portfolio').done(function (portfolios) {
            console.log('Portfolios loaded:', portfolios);
            const $portfolioId = $('#portfolioId');
            $portfolioId.empty().append('<option value="">Select Portfolio</option>');
            portfolios.forEach(function (portfolio) {
                $portfolioId.append(`<option value="${portfolio.id}">${portfolio.name}</option>`);
            });
        }).fail(function (xhr) {
            showMessage('Error', 'Failed to load portfolios: ' + (xhr.responseText || 'Unknown error'), 'error', false);
        });
    }

    function loadHoldings(portfolioId) {
        console.log('loadHoldings start:');
        if (!portfolioId) {
            $('#holdingId').empty().append('<option value="">Select Holding</option>');
            return;
        }
        $.get(`/api/Holding/portfolio/${portfolioId}`).done(function (holdings) {
            console.log('Holdings loaded for portfolio', portfolioId, ':', holdings);
            const $holdingId = $('#holdingId');
            $holdingId.empty().append('<option value="">Select Holding</option>');
            holdings.forEach(function (holding) {
                $holdingId.append(`<option value="${holding.id}">${holding.assetName}</option>`);
            });
            if (holdings.length > 0) {
                $holdingId.val(holdings[0].id);
                initTable(holdings[0].id);
                $('#formHoldingId').val(holdings[0].id);
            }
        }).fail(function (xhr) {
            showMessage('Error', 'Failed to load holdings: ' + (xhr.responseText || 'Unknown error'), 'error', false);
        });
    }

    $('#portfolioId').on('change', function () {
        const portfolioId = $(this).val();
        loadHoldings(portfolioId);
    });

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

        if (id) {
            transaction.id = parseInt(id);
        }

        if (isNaN(transaction.holdingId) || isNaN(transaction.quantity) || isNaN(transaction.price) || isNaN(transaction.amount) || isNaN(transaction.fees)) {
            showMessage('Error', 'Please fill all fields with valid numbers.', 'error', true);
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
                showMessage('Success', id ? 'Transaction updated successfully.' : 'Transaction created successfully.', 'success', false);
            },
            error: function (xhr) {
                showMessage('Error', 'Failed to save transaction: ' + (xhr.responseText || 'Unknown error'), 'error', true);
            }
        });
    });

    $('#addTransactionBtn').on('click', resetAddModal);

    $('#loadTransactionsBtn').on('click', function () {
        const holdingIdInput = $('#holdingId').val();
        const holdingId = parseInt(holdingIdInput);
        if (!holdingIdInput || isNaN(holdingId) || holdingId <= 0) {
            showMessage('Error', 'Please select a valid Holding ID.', 'error', false);
            return;
        }
        initTable(holdingId);
        $('#formHoldingId').val(holdingIdInput);
    });

    loadPortfolios();
});
const modalDialog = document.getElementById('modalDialog')





function DialogChiTiet(idBill) {
    $.ajax({
        url: '/ManagerBill/GetDetailBill',
        type: 'GET',
        data: { idBillStr: idBill },
        success: function (result) {
            if (result.status) {
                const message = result.message
                const data = JSON.parse(result.data)
                if (data) {
                    modalDialog.innerHTML =
                        `
                     <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Thông tin hóa đơn</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true"></span>
                                </button>
                            </div>

                            <div class="modal-body">

                            <div class="row">
                                    <div class="col-sm-4">
                                    <img src="${data.Consumer.Image}" style="width: 150px;" />
                                    </div>
                                    <div class="col-sm-8">
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th scope="col"></th>
                                                <th scope="col"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="table-active">
                                                <th scope="row">Tên khách hàng: </th>
                                                <td>${data.Consumer.Username}</td>
                                            </tr>
                                            <tr class="table-active">
                                                <th scope="row">Tổng hóa đơn: </th>
                                                <td>${data.Bill.TotalBill}</td>
                                            </tr>

                                            <tr class="table-active">
                                                <th scope="row">Giảm giá: </th>
                                                <td>${data.Bill.Discount} %</td>
                                            </tr>

                                            <tr class="table-active">
                                                <th scope="row">Thanh toán: </th>
                                                <td>${data.Bill.TotalPay}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    </div>
                                </div>


                                <h3>Danh sách sản phẩm</h3>
                                <table class="table table-hover">
                                    <thead>
                                    <tr>
                                        <th scope="col">Tên sản phẩm</th>
                                        <th scope="col">Số lượng</th>
                                    </tr>
                                    </thead>
                                    <tbody id="list_products">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    `;
                    var listProductsId = document.getElementById('list_products');
                    for (var i in data.ListProducts) {
                        var item = data.ListProducts[i];
                        listProductsId.innerHTML +=
                            `
                           <tr class="table-active">
                                <th scope="row">${item.NameProduct}</th>
                                <td>${item.QuanitySale}</td>
                            </tr>
                            `;
                    }
                    modalDialog.modal('show');
                }
            } else {
                // Add html when load detail bill failure

            }
        },
        error: function () {
            console.error("AJAX request failed");
        }
    });
}
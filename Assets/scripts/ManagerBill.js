const dialogDetailBill = document.getElementById('dialog_detail_bill')

function actionDialogDetailBill(billId) {
   loadDetailBill(
      billId = billId,
      onSuccess = function (
         data,
         message
      ) {
         if (data)
            showDialogDetailBill(data = data)
      },
      onFailure = function (
         message
      ) {

      }
   )
}

async function loadDetailBill(
   billId,
   onSuccess,
   onFailure
) {
    $.ajax({
      url: '/ManagerBill/DetailBill',
      type: 'POST',
      data: {
         billId: billId
      },
      success: function (result) {
         console.log(result)

         if (result.status) {
            onSuccess(
               data = result.data,
               message = result.message
            )
         } else {
            onFailure(
               message = result.message
            )
         }
      },
      error: function () {
         console.error("AJAX request failed");
      }
   });
}


function showDialogDetailBill(data) {
   dialogDetailBill.innerHTML =
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
                     <img src="${data.ConsumerImage}" style="width: 150px;" />
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
                                 <td>${data.ConsumerName}</td>
                              </tr>
                              <tr class="table-active">
                                 <th scope="row">Tổng hóa đơn: </th>
                                 <td>${data.TotalBill}</td>
                              </tr>

                              <tr class="table-active">
                                 <th scope="row">Giảm giá: </th>
                                 <td>${data.PercentDiscount}</td>
                              </tr>

                              <tr class="table-active">
                                 <th scope="row">Thanh toán: </th>
                                 <td>${data.TotalPay}</td>
                              </tr>
                              <tr class="table-active">
                              <th scope="row">Trạng thái giao hàng: </th>
                              <td>${data.StatusDelivery}</td>
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
                     <tbody id="DanhSachSanPham">
                     </tbody>
                  </table>
            </div>
         </div>
      </div>
      `
   var danhSach = document.getElementById('DanhSachSanPham');

   var detailListBill = data.DetailListBill

   for (var i in detailListBill) {
      var item = detailListBill[i];
      danhSach.innerHTML +=
         `
       <tr class="table-active">
            <th scope="row">${item.NameProduct}</th>
            <td>${item.Quantity}</td>
        </tr>
        `;
   }

   $(dialogDetailBill).modal('show');
}

function hideDialogDetailBill() {
   $(dialogDetailBill).modal('hide');
   dialogDetailBill.innerHTML = ''
}

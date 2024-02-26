




//function ShoDialogDelete(maKhuyenMai) {
//    var dialog = document.getElementById('DialogConfirm');
//    $('#ma').val(maKhuyenMai);
//    $(dialog).modal('show');
//}


//function ActionDelete() {
//    var maKhuyenMai = $('#ma').val();
//    $.ajax({
//        url: '/KhuyenMais/Delete',
//        type: 'POST',
//        data: { maKhuyenMai: maKhuyenMai },
//        success: function (data) {
//            if (data.status) {
//                var dialog = document.getElementById('DialogConfirm');
//                $(dialog).modal('hide');
//                location.reload();
//            } else {
//                toastr.warning('Khuyến mãi', `${data.message}`)
//            }
//        },
//        error: function () {
//            console.error("AJAX request failed");
//        }
//    });
//}
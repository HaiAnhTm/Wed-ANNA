// Hàm sử dụng khi nhấn nút thanh toán
function ThanhToan() {
    var hoaDon = ;
    $.ajax({
        url: '@Url.Action("ThanhToan", "BanHang")',
        type: 'POST',
        data: { hoaDonJson: `${JSON.stringify(hoaDon)}` },

        success: function(result) {
            console.log(result);
            if (result.status) {
                toastr.options.closeButton = true;
                toastr.success(result.message, "Thanh toán");
                location.reload();
            } else {
                //window.location.href = result.url;
                toastr.options.closeButton = true;
                toastr.warning(result.message, "Thanh toán");
            }
        },
        error: function() {
            console.error("AJAX request failed");
        }
    });
}

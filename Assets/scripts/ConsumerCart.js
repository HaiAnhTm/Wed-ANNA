
function actionPayBill() {
    $.ajax({
        url: '/ConsumerProduct/ConsumerCart',
        type: 'POST',
        data: {},
        success: function (result) {
            console.log(result);
            if (result.status) {
                toastr.options.closeButton = true;
                toastr.success(result.message, "Thanh toán")
                location.reload();
            } else {
                //window.location.href = result.url;
                toastr.options.closeButton = true;
                toastr.warning(result.message, "Thanh toán")
            }
        },
        error: function () {
            console.error("AJAX request failed");
        }
    });
}

// Hàm sử dụng để hủy thanh toán hóa đơn này và xóa toàn bộ sản phẩm
function HuyThanhToan() {
    $.ajax({
        url: '@Url.Action("HuyThanhToan", "BanHang")',
        type: 'POST',
        data: {},
        success: function (result) {
            if (result.status) {
                toastr.options.closeButton = true;
                toastr.success(result.message, 'Giỏ hàng');
                setTimeout(function () {
                    location.reload();
                }, 500);
            } else {
                toastr.options.closeButton = true;
                toastr.warning(result.message, 'Giỏ hàng');
            }
        }
    });
}
function createFormPost(data) {
    var form = document.createElement('form');
    form.action = '@Url.Action("ConsumerCart", "ConsumerProduct")';
    form.method = 'POST';
    for (var key in data) {
        if (data.hasOwnProperty(key)) {
            var input = document.createElement('input');
            input.type = 'hidden';
            input.name = key;
            input.value = data[key];
            form.appendChild(input);
        }
    }
    document.body.appendChild(form);
    form.submit();
}
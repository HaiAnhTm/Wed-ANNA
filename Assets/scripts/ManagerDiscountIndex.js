const filterSelectedId = document.getElementById('fillter_selected');

document.addEventListener('DOMContentLoaded', function () {
    loadSortIndex();
});

filterSelectedId.addEventListener('change', function (event) {
    var selectedValue = this.value; 
    console.log(selectedValue);
    location.href = `/ManagerDiscount/Index?sort=${selectedValue}`;
});

function loadSortIndex() {
    var urlParams = new URLSearchParams(window.location.search);
    var sortParam = urlParams.get('sort');

    if (!isNaN(sortParam) && sortParam !== -1) {
        filterSelectedId.value = sortParam; 
    }
}

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
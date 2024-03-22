const filterSelectedId = document.getElementById('fillter_selected');

document.addEventListener('DOMContentLoaded', function () {
    loadSortIndex();
});

filterSelectedId.addEventListener('change', function (event) {
    var selectedValue = this.value;
    console.log(selectedValue);
    location.href = `/ManagerProduct/Index?sort=${selectedValue}`;
});

function loadSortIndex() {
    var urlParams = new URLSearchParams(window.location.search);
    var sortParam = urlParams.get('sort');

    if (!isNaN(sortParam) && sortParam !== -1) {
        filterSelectedId.value = sortParam;
    }
}

function handleIconUpdate(event) {
    event.preventDefault();
    var link = event.target.parentElement.querySelector('.icon-update');
    if (link) {
        link.click();
    }
}
function handleIconDelete(event) {
    event.preventDefault();
    var link = event.target.parentElement.querySelector('.icon-delete');
    if (link) {
        link.click();
    }
}
const dialogConfirmId = document.getElementById('dialog_confirm');
const buttonConfirmId = document.getElementById('button_delete')
function hideDialogConfirm() {
    $(dialogConfirmId).modal('hide');
}

function showDiaLogConfirm(IdProduct) {
    $(dialogConfirmId).modal('show');

    buttonConfirmId.addEventListener('click', function() {
        $.ajax({
            url: '/ManagerProduct/DeleteProduct',
            type: 'POST',
            data: { IdProduct: IdProduct },
            success: function (data) {
                if (data) {
                    hideDialogConfirm()
                    location.reload();
                } else {
                    console.error("Update failed.");
                }
            },
            error: function (error) {
                console.log("Error: ", error);
            }
        });
    })
    buttonConfirmId.removeEventListener('click')
    
}


document.addEventListener("DOMContentLoaded", function () {
    // đổi lại placehoder khi được select
    var selectElement = document.getElementById("find_product_select");
    var inputElement = document.getElementById("productNameInput");

    selectElement.addEventListener("change", function () {
        if (selectElement.value === "0") {
            inputElement.placeholder = "Nhập Tên Sản Phẩm";
        } else if (selectElement.value === "1") {
            inputElement.placeholder = "Nhập Mã Sản Phẩm";
        }
    });
    // reset button
    var resetButton = document.getElementById("resetButton");

    resetButton.addEventListener("click", function () {
        var inputs = document.querySelectorAll('.find-product-name-input');
        inputs.forEach(function (input) {
            input.value = '';
        });
    });
});
// Hàm để cập nhật số lượng sản phẩm đã được chọn trong footer
function updateSelectedProductCount() {
    var selectedCount = document.querySelectorAll('.content-body-name-checkbox:checked').length;
    var footerText = document.querySelector('.footer-chose-right-text');
    footerText.textContent = selectedCount + " sản phẩm đã được chọn";
}

// Hàm để kiểm tra xem tất cả các checkbox đã được chọn hay chưa
function checkAllSelected() {
    var checkboxes = document.querySelectorAll('.content-body-name-checkbox');
    for (var i = 0; i < checkboxes.length; i++) {
        if (!checkboxes[i].checked) {
            return false;
        }
    }
    return true;
}

// Hàm để kiểm tra xem có bất kỳ checkbox nào được chọn không
function checkAnySelected() {
    var checkboxes = document.querySelectorAll('.content-body-name-checkbox');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            return true;
        }
    }
    return false;
}

// Hàm để hiển thị hoặc ẩn phần footer
function toggleFooter() {
    var footer = document.querySelector('.footer-chose');
    var footerCheckbox = document.querySelector('.footer-chose-left-checkbox');
    if (checkAnySelected()) {
        footer.style.display = 'flex';
        footerCheckbox.checked = checkAllSelected();
    } else {
        footer.style.display = 'none';
        footerCheckbox.checked = false;
    }
}

// Hàm để cập nhật trạng thái checkbox header
function updateHeaderCheckbox() {
    var headerCheckbox = document.querySelector('.header-text-product input[type="checkbox"]');
    headerCheckbox.checked = checkAllSelected();
}

// Lắng nghe sự kiện click trên các checkbox và tiêu đề sản phẩm
var checkboxes = document.querySelectorAll('.content-body-name-checkbox');
for (var i = 0; i < checkboxes.length; i++) {
    checkboxes[i].addEventListener('click', function () {
        toggleFooter();
        updateHeaderCheckbox();
        updateSelectedProductCount();
    });
}

// Lắng nghe sự kiện click trên checkbox footer để chọn tất cả hoặc bỏ chọn tất cả
var footerCheckbox = document.querySelector('.footer-chose-left-checkbox');
footerCheckbox.addEventListener('click', function () {
    var checkboxes = document.querySelectorAll('.content-body-name-checkbox');
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = footerCheckbox.checked;
    }
    toggleFooter();
    updateHeaderCheckbox();
    updateSelectedProductCount();
});

// Lắng nghe sự kiện click trên checkbox header để chọn tất cả hoặc bỏ chọn tất cả
var headerCheckbox = document.querySelector('.header-text-product input[type="checkbox"]');
headerCheckbox.addEventListener('click', function () {
    var checkboxes = document.querySelectorAll('.content-body-name-checkbox');
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = headerCheckbox.checked;
    }
    toggleFooter();
    updateSelectedProductCount();
});
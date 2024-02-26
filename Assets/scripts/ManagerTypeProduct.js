const dialogAddId = document.getElementById('modal_dialog_add')

const addInputNameId = document.getElementById('add_input_type_product_name')

const notifyInputId = document.getElementById('notify_input')


addInputNameId.addEventListener('click', function () {
    removeTextContent(notifyInputId)
})

addInputNameId.addEventListener('blur', function () {
    if (inputEmpty(notifyInputId, addInputNameId.value))
        return
})

function showDialogAdd() {
    $(dialogAddId).modal('show');
}
function hideDialogAdd() {
    $(dialogAddId).modal('hide');
}

function actionAddTypeProduct() {
    const inputName = addInputNameId.value
    if (inputEmpty(notifyInputId, addInputNameId.value))
        return
    addTypeProduct(
        typeProductName = inputName,
        success = function () {
            hideDialogAdd()
            location.reload()
        },
        failure = function (message) {
            // Toast error

        }
    )
}


















function inputEmpty(notifyId, input) {
    console.log(input)
    if (input.trim() === '') {
        notifyId.textContent = "Vui lòng nhập vào ô trống!"
        return true
    }
    return false
}

function removeTextContent(tagId) {
    tagId.textContent = ''
}

function addTypeProduct(
    typeProductName,
    success,
    failure
) {
    $.ajax({
        url: '/ManagerTypeProduct/AddTypeProduct',
        type: 'POST',
        data: {
            IdTypeProduct: null,
            TypeProductName: typeProductName
        },
        success: function (data) {
            if (data.status) {
                console.log(data.message)
                success()
            } else 0
            if (data.message.length > 0)
                failure(data.message)
        },
        error: function () {
            console.error("AJAX request failed");
        }
    });
}


function DialogThayDoi(maLoai, tenLoai) {
    var dialogThayDoi = document.getElementById('dialogThayDoi');
    // Gan gia tri cho form
    $('#MaLoaiSanPham').val(maLoai);
    $('#TenLoaiSanPham').val(tenLoai);
    // Show dialog
    $(dialogThayDoi).modal('show');
}


function ActionCapNhat() {
    var maLoai = document.getElementById('MaLoaiSanPham').value;
    var tenLoai = document.getElementById('TenLoaiSanPham').value;
    if (tenLoai.length === 0) {
        toastr.options.closeButton = true;
        toastr.error('Cập nhật loại sản phẩm', 'Tên sản phẩm bị rỗng!')
    } else
        $.ajax({
            url: '/LoaiSanPham/CapNhatLoaiSanPham',
            type: 'POST',
            data: { MaLoaiSanPham: maLoai, TenLoaiSanPham: tenLoai },
            success: function (data) {
                if (data) {
                    var dialogThayDoi = document.getElementById('dialogThayDoi');
                    $(dialogThayDoi).modal('hide');
                    location.reload();
                } else {
                    console.error("Update failed.");
                }
            },
            error: function () {
                console.error("AJAX request failed");
            }
        });
}
function ActionXoa() {
    var maLoai = document.getElementById('MaLoaiSanPham').value;
    $.ajax({
        url: '@Url.Action("XoaLoaiSanPham", "LoaiSanPham")',
        type: 'POST',
        data: { MaLoaiSanPham: maLoai },
        success: function (data) {
            if (data.status) {
                console.log("Delete successful.");
                var dialogThayDoi = document.getElementById('dialogThayDoi');
                $(dialogThayDoi).modal('hide');
                location.reload();
            } else {
                toastr.warning('Loại sản phẩm', `${data.message}`);
            }
        },
        error: function () {
            console.error("AJAX request failed");
        }
    });
}
function ActionThem() {
    var tenLoai = document.getElementById("TenLoaiSanPhamThem").value;
    $.ajax({
        url: '/LoaiSanPham/ThemLoaiSanPham',
        type: 'POST',
        data: { TenLoaiSanPham: tenLoai },
        success: function (data) {
            if (data) {
                console.log("Create successful.");
                var dialogThem = document.getElementById('DialogThem');
                $(dialogThem).modal('hide');
                location.reload();
            } else {
                console.error("Create failed.");
            }
        },
        error: function () {
            console.error("AJAX request failed");
        }
    });
}
const dialogAddId = document.getElementById('modal_dialog_add')
const dialogUpdateId = document.getElementById('modal_dialog_update')

const addInputNameId = document.getElementById('add_input_type_product_name')
const updateInputIdId = document.getElementById('update_input_type_product_id')
const updateInputNameId = document.getElementById('update_input_type_product_name')

const notifyInputAddId = document.getElementById('notify_input_add')
const notifyInputUpdateId = document.getElementById('notify_input_update')



addInputNameId.addEventListener('click', function () {
    removeTextContent(notifyInputAddId)
})

addInputNameId.addEventListener('blur', function () {
    if (inputEmpty(notifyInputAddId, addInputNameId.value))
        return
})

updateInputNameId.addEventListener('click', function () {
    removeTextContent(notifyInputUpdateId)
})

updateInputNameId.addEventListener('blur', function () {
    if (inputEmpty(notifyInputUpdateId, updateInputNameId.value))
        return
})

function actionAddTypeProduct() {
    const inputName = addInputNameId.value
    if (inputEmpty(notifyInputAddId, addInputNameId.value))
        return
    addTypeProduct(
        typeProductName = inputName,
        success = function (message) {
            hideDialogAdd()
            location.reload()
        },
        failure = function (message) {
            // Toast error

        }
    )
}

function actionUpdateTypeProduct() {
    if (inputEmpty(notifyInputUpdateId, updateInputNameId.value))
        return
    const idTypeProduct = updateInputIdId.value
    const nameTypeProduct = updateInputNameId.value
    updateTypeProduct(
        idTypeProduct,
        nameTypeProduct,
        success = function (message) {
            hideDialogUpdate()
            location.reload()
        },
        failure = function (message) {

        }
    )
}

function actionDeleteTypeProduct() {
    const idTypeProduct = updateInputIdId.value
    deleteTypeProduct(
        IdTypeProduct = idTypeProduct,
        success = function (message) {
            hideDialogUpdate()
            location.reload()
        },
        failure = function (message) {

        }
    )
}

function showDialogAdd() {
    $(dialogAddId).modal('show');
}

function hideDialogAdd() {
    $(dialogAddId).modal('hide');
}

function showDialogUpdate(IdTypeProduct, TypeProductName) {
    $(updateInputIdId).val(IdTypeProduct)
    $(updateInputNameId).val(TypeProductName)
    $(dialogUpdateId).modal('show');
}

function hideDialogUpdate() {
    $(dialogUpdateId).modal('hide');
    $(updateInputIdId).val(null)
    $(updateInputNameId).val(null)
}

function inputEmpty(notifyId, input) {
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
            if (data.status)
                success(data.message)
            else
                failure(data.message)
        },
        error: function () {
            console.error("AJAX request failed");
            failure("Thêm thất bại!")
        }
    });
}

function updateTypeProduct(
    IdTypeProduct,
    TypeProductNameNew,
    success,
    failure
) {
    $.ajax({
        url: '/ManagerTypeProduct/UpdateTypeProduct',
        type: 'POST',
        data: {
            IdTypeProduct: IdTypeProduct,
            TypeProductName: TypeProductNameNew
        },
        success: function (data) {
            if (data.status)
                success(data.message)
            else
                failure(data.message)
        },
        error: function () {
            console.error("AJAX request failed");
            failure("Cập nhật thất bại!")
        }
    });
}
function deleteTypeProduct(
    IdTypeProduct,
    success,
    failure
) {
    $.ajax({
        url: '/ManagerTypeProduct/DeleteTypeProduct',
        type: 'POST',
        data: {
            IdTypeProduct: IdTypeProduct
        },
        success: function (data) {
            if (data.status)
                success(data.message)
            else
                failure(data.message)
        },
        error: function () {
            console.error("AJAX request failed");
            failure("Xóa thất bại!")
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
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
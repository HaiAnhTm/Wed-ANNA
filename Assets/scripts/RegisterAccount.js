const inputUsername = document.getElementById('input-username')
const inputPassword = document.getElementById('input-password')
const inputPasswordConfirm = document.getElementById('input-password-confirm')
const inputName = document.getElementById('input-name')

const notifyUsername = document.getElementById('notify_username')
const notifyPassword = document.getElementById('notify_password')
const notifyPasswordConfirm = document.getElementById('notify_password_confirm')
const notifyName = document.getElementById('notify_name')

const buttonRegister = document.getElementById('button_register')
const spinnerButton = document.getElementById('spinner_button')

const form = document.getElementById('form-signIn')

inputUsername.addEventListener('click', function () {
    removeTextContent(notifyUsername)
})
inputPassword.addEventListener('click', function () {
    removeTextContent(notifyPassword)
})
inputPasswordConfirm.addEventListener('click', function () {
    removeTextContent(notifyPasswordConfirm)
})
inputName.addEventListener('click', function () {
    removeTextContent(notifyName)
})

inputUsername.addEventListener('blur', function (event) {
    if (inputEmpty(notifyUsername, inputUsername.value))
        return
    if (checkUsernameExist(inputUsername.value, notifyUsername))
        return
})

inputPassword.addEventListener('blur', function (event) {
    if (inputEmpty(notifyPassword, inputPassword.value))
        return
    if (inputPasswordShort(notifyPassword, inputPassword.value))
        return
})

inputPasswordConfirm.addEventListener('blur', function (event) {
    if (inputEmpty(notifyPasswordConfirm, inputPasswordConfirm.value)) {
        return;
    }
    if (inputPasswordShort(notifyPasswordConfirm, inputPasswordConfirm.value))
        return
    if (checkPasswordConfirmNotEquals())
        return
})

inputName.addEventListener('blur', function (event) {
    if (inputEmpty(notifyName, inputName.value))
        return
})

buttonRegister.addEventListener('click', function (event) {
    loadRegisterButton()
    registerAccount()
})

function inputEmpty(notifyId, input) {
    if (input.trim().length == 0) {
        notifyId.textContent = "Vui lòng nhập vào ô trống!"
        return true
    }
    return false
}
function inputPasswordShort(notifyId, input) {
    if (input.trim().length < 8) {
        notifyId.textContent = "Mật khẩu quá ngắn!"
        return true
    }
    return false
}

function removeTextContent(tagId) {
    tagId.textContent = ''
}

async function checkUsernameExist(userName, notifyId) {
    $.ajax({
        url: '/RegisterAccount/CheckExistUsername',
        type: 'POST',
        data: { Username: userName },
        success: function (result) {
            if (result.status) {
                notifyId.textContent = result.message
                return true
            }
            else
                return false
        },
        error: function (error) {

        }
    })
}

function checkPasswordConfirmNotEquals() {
    if (inputPassword.value != inputPasswordConfirm.value) {
        notifyPasswordConfirm.textContent = "Mật khẩu không trùng khớp!"
        return true
    }
    return false
}

function loadRegisterButton() {
    spinnerButton.classList.add("spinner-grow")
}

function unLoadRegisterButton() {
    spinnerButton.classList.remove('spinner-grow')
}

async function registerAccount() {
    if (
        notifyUsername.textContent.length == 0 &&
        notifyPassword.textContent.length == 0 &&
        notifyPasswordConfirm.textContent.length == 0 &&
        notifyName.textContent.length == 0
    ) {
        form.submit()
    }
    else {
        setTimeout(() => {
            toastr.options.closeButton = true;
            toastr.warning("Vui lòng nhập đầy đủ thông tin!", 'Đăng ký')
            unLoadRegisterButton()
        }, 300);
    }
}
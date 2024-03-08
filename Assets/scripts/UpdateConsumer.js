const dialogChangePasswordId = document.getElementById('dialog_change_password')

var inputAccountId = document.getElementById('account_id');
var inputPasswordOld = document.getElementById('password_old');
var inputPasswordNew = document.getElementById('password_new');
var inputPasswordRepeat = document.getElementById('password_repeat');

var notifyPasswordOld = document.getElementById('notify_password_old');
var notifyPasswordNew = document.getElementById('notify_password_new');
var notifyPasswordRepeat = document.getElementById('notify_password_repeat');
var notifyStatus = document.getElementById('notify_status')

inputPasswordOld.addEventListener('click', function () {
   removeTextContent(notifyPasswordOld)
   removeTextContent(notifyStatus)
})

inputPasswordOld.addEventListener('blur', function (event) {
   if (inputEmpty(notifyPasswordOld, inputPasswordOld.value))
      return
})

inputPasswordNew.addEventListener('click', function () {
   removeTextContent(notifyPasswordNew)
   removeTextContent(notifyStatus)
})

inputPasswordNew.addEventListener('blur', function (event) {
   if (inputEmpty(notifyPasswordNew, inputPasswordNew.value))
      return
   if (inputPasswordShort(notifyPasswordNew, inputPasswordNew.value))
      return
})

inputPasswordRepeat.addEventListener('click', function () {
   removeTextContent(notifyPasswordRepeat)
   removeTextContent(notifyStatus)
})

inputPasswordRepeat.addEventListener('blur', function (event) {
   if (inputEmpty(notifyPasswordRepeat, inputPasswordRepeat.value))
      return
   if (inputPasswordShort(notifyPasswordRepeat, inputPasswordRepeat.value))
      return
   checkPasswordConfirmNotEquals()
})


function removeTextContent(tagId) {
   tagId.textContent = ''
}

function inputEmpty(notifyId, input) {
   console.log(input)
   if (input.trim() === '') {
      notifyId.textContent = "Vui lòng nhập vào ô trống!"
      return true
   }
   return false
}
function checkPasswordConfirmNotEquals() {
   if (inputPasswordNew.value != inputPasswordRepeat.value) {
      notifyPasswordRepeat.textContent = "Mật khẩu không trùng khớp!"
      return true
   }
   return false
}
function inputPasswordShort(notifyId, input) {
   if (input.trim().length < 6   ) {
      notifyId.textContent = "Mật khẩu quá ngắn!"
      return true
   }
   return false
}

function actionUpdatePassword() {
   if (
      notifyPasswordOld.textContent.length == 0 &&
      notifyPasswordNew.textContent.length == 0 &&
      notifyPasswordRepeat.textContent.length == 0 &&
      notifyStatus.textContent.length == 0
   ) {

      $.ajax({
         url: '/ManagerConsumer/UpdatePassword',
         type: 'POST',
         data: {
            accountId: inputAccountId.value,
            oldPassword: inputPasswordOld.value,
            newPassword: inputPasswordNew.value
         },
         success: function (data) {
            if (data.status) {
               toastr.success(data.message, 'Cập nhật mật khẩu')
               hideDialogChangePassword()
            } else {
               toastr.error(data.message, 'Cập nhật mật khẩu')
            }
         },
         error: function () {
            console.error("AJAX request failed");
         }
      })

   } else {
      notifyStatus.textContent = "Vui lòng nhập vào ô trống!"
   }
}

function showDialogChangePassword() {
   $(dialogChangePasswordId).modal('show');
}

function hideDialogChangePassword() {
   $(dialogChangePasswordId).modal('hide');
}
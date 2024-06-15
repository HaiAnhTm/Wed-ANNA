const inputUsername = document.getElementById('input_username')
const inputPassword = document.getElementById('input_password')

const buttonLogin = document.getElementById('button-login')
const spinnerButton = document.getElementById('spinner_button')

const notifyUsername = document.getElementById('notify_username')
const notifyPassword = document.getElementById('notify_password')
const notifyStatus = document.getElementById('notify_status')

inputUsername.addEventListener('click', function () {
   removeTextContent(notifyUsername)
   removeTextContent(notifyStatus)
})
inputPassword.addEventListener('click', function () {
   removeTextContent(notifyPassword)
   removeTextContent(notifyStatus)
})

inputUsername.addEventListener('blur', function (event) {
   if (inputEmpty(notifyUsername, inputUsername.value))
      return
})

inputPassword.addEventListener('blur', function (event) {
   if (inputEmpty(notifyPassword, inputPassword.value))
      return
})

buttonLogin.addEventListener('click', function () {
   loadLogInButton()
   if (
      notifyUsername.textContent.length == 0 &&
      notifyPassword.textContent.length == 0) {
      loginAccountSuccess(
         inputUsername.value,
         inputPassword.value,
         onSuccess = function (result) {
            setTimeout(() => {
               toastr.options.closeButton = true;
               toastr.success(result.message, 'Đăng nhập')
               console.log("Result: " + result.url)
               if (result.url.length > 0)
                  document.location.href = result.url
            }, 300)
         },
         onFailure = function (result) {
            unLoadLogInButton()
            notifyStatus.textContent = result.message
         }
      )

   } else {
      unLoadLogInButton()
      setTimeout(() => {
         toastr.options.closeButton = true;
         toastr.warning("Vui lòng nhập đầy đủ thông tin!", 'Đăng nhập')
      }, 300);
   }
})

async function loginAccountSuccess(userName, password, onSuccess, onFailure) {
   $.ajax({
      url: '/LoginAccount',
      type: 'POST',
      data: {
         Username: userName,
         Password: password
      },
      success: function (result) {
         if (result.status) {
            onSuccess(result)
            return true
         }
         else {
            onFailure(result)
            return false;
         }
      },
      error: function (error) {
         return false
      }
   })
}

function loadLogInButton() {
   spinnerButton.classList.add("spinner-grow")
}

function unLoadLogInButton() {
   spinnerButton.classList.remove('spinner-grow')
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

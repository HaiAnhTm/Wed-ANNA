const buttonCreateCode = document.getElementById('button_create_code')
const inputCreateCode = document.getElementById('code_discount')

buttonCreateCode.addEventListener('click', function (event) {
    console.log('click')
    getCodeDiscount(
        onSuccess = function (code, message) {
            inputCreateCode.value = code
        },
        onFailure = function () {

        }
    )
})


async function getCodeDiscount(
    onSuccess,
    onFailure
) {
    $.ajax({
        url: '/ManagerDiscount/GetCodeDiscount',
        type: 'POST',
        data: {
        },
        success: function (result) {
            if (result.status) {
                console.log(result.code)
                onSuccess(result.code, result.message)
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
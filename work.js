async function deleteDiscount(
  id_discount, 
  onSuccess,
  onFailure
) {
  try {
    const response = await $.ajax({
      url: "/ManagerDiscount/DeleteDiscount",
      type: "DELETE",
      data: {
        id_discount: id_discount,
      },
    });

    if (response.status) {
      onSuccess(response.message);
    } else {
      onFailure(response.message)
    }
  } catch (error) {
    console.error("Error:", error);
    onFailure();
  }
}

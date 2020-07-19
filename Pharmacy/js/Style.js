$("#btnCreate").click(function (eve) {
    $("#modal-content").load("/Products/Create");
});

$(".btnEdit").click(function (eve) {
    $("#modal-content").load("/Products/Edit/" + $(this).data("id"));
});                           

$(".btnDetails").click(function (eve) {        
    $("#modal-content").load("/Products/Details/" + $(this).data("id"));
});                                            

$(".btnDelete").click(function (eve) {         
    $("#modal-content").load("/Products/Delete/" + $(this).data("id"));
});

$("#btnRegister").click(function (eve) {
    $("#modal-content").load("/Account/Register/");
});

$("#btnLogin").click(function (eve) {
    $("#modal-content").load("/Account/Login/");
});

$(".btnSell").click(function (eve) {
    $("#modal-content").load("/Operations/Index/");
});

$("#btnObtainingClient").click(function (eve) {
    $("#modal-content").load("/Sale/ObtainingClient/");
});

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

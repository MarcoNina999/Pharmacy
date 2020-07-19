var printThhisDiv = (id) => {
    var printContents = document.getElementById(id).innerHTML;
    var originalContents = docuent.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}
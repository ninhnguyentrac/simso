function checkFileExtension() {
    var ext = $("#fileUpload").val().split('.').pop();
    if(ext!="xls"&& ext!="xlsx"&&ext!="csv") {
        alert("File không đúng.");
        return false;
    }
    return true;
}
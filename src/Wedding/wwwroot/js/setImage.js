function setImage(imgId, data) {
    var reader = new FileReader();
    reader.onload = function (e) {
        var img = document.getElementById(imgId);
        img.src = e.target.result;
    };
    reader.readAsDataURL(data);
}
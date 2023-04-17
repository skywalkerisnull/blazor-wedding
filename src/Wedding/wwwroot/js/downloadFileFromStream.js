window.downloadFileFromStream = (bytes, fileName) => {
    // Create a blob from the byte array
    var blob = new Blob([bytes]);
    // Create an object URL for the blob
    var url = URL.createObjectURL(blob);
    // Create an anchor element with the file name and URL
    var a = document.createElement('a');
    a.download = fileName;
    a.href = url;
    // Invoke the click method of the anchor element
    a.click();
};
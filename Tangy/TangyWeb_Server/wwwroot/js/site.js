window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Uspješna akcija', { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, 'Neuspješna akcija', { timeOut: 5000 });
    }
}
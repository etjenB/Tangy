window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Uspješna akcija', { timeOut: 5000 });
    }
    if (type === "error") {
        toastr.error(message, 'Neuspješna akcija', { timeOut: 5000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire({
            title: "Uspjeh!",
            text: message,
            icon: "success"
        });
    }
    if (type === "error") {
        Swal.fire({
            title: "Greška!",
            text: message,
            icon: "error"
        });
    }
}

function ShowDeleteConfirmationModal() {
    $("#deleteConfirmationModal").modal("show");
}

function HideDeleteConfirmationModal() {
    $("#deleteConfirmationModal").modal("hide");
}
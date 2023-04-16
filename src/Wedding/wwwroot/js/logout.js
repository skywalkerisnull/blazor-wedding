function logout() {
    fetch("/Identity/Account/Logout", { method: "POST" })
        .then(response => {
            if (response.ok) {
                window.location.href = "/login";
            }
        });
}

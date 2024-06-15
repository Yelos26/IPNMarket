document.addEventListener("DOMContentLoaded", function () {
    var loadingScreen = document.getElementById("loading-screen");
    var content = document.getElementById("content");

    // Verificar si estamos en la acción Home/Index
    if (window.location.pathname === "/Home/Index" || window.location.pathname === "/") {
        // Comprobar si ya hemos mostrado la pantalla de carga antes
        var hasVisitedBefore = localStorage.getItem("hasVisitedBefore");

        if (!hasVisitedBefore) {
            // Mostrar el loading screen durante 3 segundos la primera vez
            setTimeout(function () {
                loadingScreen.style.display = "none";
                content.style.display = "block";
                // Marcar que hemos mostrado la pantalla de carga
                localStorage.setItem("hasVisitedBefore", "true");
            }, 1000); // 3000ms = 3 segundos
        } else {
            // Ocultar el loading screen y mostrar el contenido inmediatamente
            loadingScreen.style.display = "none";
            content.style.display = "block";
        }
    } else {
        // Ocultar el loading screen y mostrar el contenido inmediatamente
        loadingScreen.style.display = "none";
        content.style.display = "block";
    }
});

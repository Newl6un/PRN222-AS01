// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const toastManager = {
    showToast: function (message, title = 'Notification', type = 'info') {
        const toastContainer = document.getElementById('toastContainer');
        if (!toastContainer) {
            console.error('Toast container not found!');
            return;
        }

        const toastId = `toast-${Date.now()}`;
        const toastHtml = `
            <div id="${toastId}" class="toast align-items-center text-white bg-${type} border-0" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="d-flex">
                    <div class="toast-body">
                        <strong>${title}</strong>
                        <div>${message}</div>
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        `;

        toastContainer.insertAdjacentHTML('beforeend', toastHtml);

        const toast = new bootstrap.Toast(document.getElementById(toastId), {
            delay: 5000
        });
        toast.show();

        // Remove toast from DOM after it's hidden
        document.getElementById(toastId).addEventListener('hidden.bs.toast', function () {
            this.remove();
        });
    }
};
const modalDialogManager = {
    loadFormModal: function (url, modalId) {
        $.get(url).done(function (data) {
            $(modalId).find('.modal-body').html(data);
            $(modalId).modal('show');
            initCKEditor();
        });
    },

    submitFormModal: function (form, modalId) {
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    $(modalId).modal('hide');
                    // Show success toast
                    // toastManager.showToast('Operation completed successfully!', 'Success', 'success');
                    // Reload the page after a short delay
                    setTimeout(function () {
                        window.location.reload();
                    }, 1000);
                } else {
                    $(modalId).find('.modal-body').html(response);
                    initCKEditor();
                }
            },
            error: function (xhr) {
                toastManager.showToast(xhr.responseText, 'Failed', 'danger');
                $(modalId).modal('hide');

            }
        });
        return false;
    }
};
function initCKEditor() {
    const textarea = document.querySelector('#NewsContent');
    if (textarea) {
        ClassicEditor
            .create(textarea)
            .then(editor => {
                window.newsEditor = editor;
            })
            .catch(error => {
                console.error('CKEditor error:', error);
            });
    }
}



$(document).ready(function () {
    // Handle modal form submissions
    $(document).on('submit', '.modal-body form', function (e) {
        e.preventDefault();
        modalDialogManager.submitFormModal($(this), '#commonModal');
    });
});

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/newsHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveNewsUpdate", function (message) {
    // alert("📢 News Update: " + message);
    location.reload();
});

connection.on("ReceiveNewsEdit", function (message) {
    //alert("✏️ News Edited: " + message);
    location.reload();
});

connection.on("ReceiveNewsDelete", function (message) {
    //alert("🗑️ News Deleted: " + message);
    location.reload();
});

connection.start()
    .then(() => console.log("✅ Connected to SignalR"))
    .catch(err => console.error("❌ SignalR Connection Error: ", err));
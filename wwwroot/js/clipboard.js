// wwwroot/js/clipboard.js
window.copyToClipboard = (text, buttonId) => {
    navigator.clipboard.writeText(text).then(() => {
        showCopiedIcon(buttonId);
    }, (err) => {
        console.error('Copy to clipboard failed', err);
    });
}

function showCopiedIcon(buttonId) {
    const button = document.getElementById(buttonId);
    if (button) {
        const originalContent = button.innerHTML;
        button.innerHTML = '<i class="fas fa-check me-1"></i> Copied';
        button.disabled = true;

        // Revert back after 2 seconds
        setTimeout(() => {
            button.innerHTML = originalContent;
            button.disabled = false;
        }, 2000);
    }
}
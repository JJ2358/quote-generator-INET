document.addEventListener('DOMContentLoaded', function() {
    const addForm = document.getElementById('addQuoteForm');
    const deleteButton = document.getElementById('deleteQuoteButton');
    const deleteSelect = document.getElementById('deleteQuoteSelect');

    // Handle Add Quote Form Submission
    addForm.addEventListener('submit', function(event) {
        event.preventDefault();
        const formData = new FormData(addForm);
        // Add logic to handle file upload if necessary

        fetch('/api/quotes', {
            method: 'POST',
            body: JSON.stringify({
                author: formData.get('author'),
                quoteText: formData.get('quoteText'),
                permalink: formData.get('permalink') // Handle optional permalink
                // Add image handling if necessary
            }),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => {
            if (response.ok) {
                // Handle successful addition
                window.location.reload(); // Reload page to show new quote
            } else {
                // Handle error
                alert('Error adding quote');
            }
        });
    });

    // Handle Delete Quote
    deleteButton.addEventListener('click', function() {
        const selectedQuoteId = deleteSelect.value;
        fetch(`/api/quotes/${selectedQuoteId}`, {
            method: 'DELETE'
        }).then(response => {
            if (response.ok) {
                // Handle successful deletion
                window.location.reload(); // Reload page to update list
            } else {
                // Handle error
                alert('Error deleting quote');
            }
        });
    });
});

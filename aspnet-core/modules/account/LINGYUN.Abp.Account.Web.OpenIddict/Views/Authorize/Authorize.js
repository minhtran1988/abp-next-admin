$(function () {
    // Initialize the selected Scope
    updateSelectedScopes();

    // Monitor Scope selection changes
    $('.scope-checkbox').change(function () {
        updateSelectedScopes();
    });

    function updateSelectedScopes() {
        var selected = [];

        // Get the required Scope
        $('input[name="selectedScopeValues"]').each(function () {
            selected.push($(this).val());
        });

        // Get the Scope selected by the user
        $('.scope-checkbox:checked').each(function () {
            var value = $(this).data('scope-value');
            if (!selected.includes(value)) {
                selected.push(value);
            }
        });

        // Update hidden fields
        $('#selected_scopes').val(selected.join(' '));
    }
});

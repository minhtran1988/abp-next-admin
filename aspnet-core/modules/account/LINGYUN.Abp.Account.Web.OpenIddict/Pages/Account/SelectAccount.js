$(function () {
    var l = abp.localization.getResource('AbpOpenIddict');

    // Account selection interaction - using Bootstrap's list-group-item-action
    $('.account-item').on('click', function (e) {
        // If the click is a link or button, do not process it
        if ($(e.target).is('a, button, .btn') ||
            $(e.target).closest('a, button, .btn').length) {
            return;
        }

        // Remove all active states
        $('.account-item').removeClass('active');

        // Add the current selected state
        $(this).addClass('active');

        // Select the corresponding radio button
        var radio = $(this).find('input[type="radio"]');
        radio.prop('checked', true);
    });

    // form submission verification
    $('#selectAccountForm').on('submit', function (e) {
        var selectedAccount = $('input[name="Input.SelectedAccountId"]:checked').val();

        if (!selectedAccount) {
            e.preventDefault();
            abp.message.warn(l('SelectedAccountId'));
            return false;
        }

        return true;
    });

    // Initially select the first account (if it is not the current account)
    if (!$('.account-item.active').length) {
        $('.account-item:first').trigger('click');
    }

    // Automatically focus on the selected account
    $('.account-item.active').find('input[type="radio"]').focus();
});

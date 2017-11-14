function registerDialog(dialogId, actionBtnId,parent) {
    if (actionBtnId.isIdSelector()) {

        var dialog = document.querySelector(dialogId);
        var btn = document.querySelector(actionBtnId);

        if (!dialog.showModal) {
            dialogPolyfill.registerDialog(dialog);
        }

        btn.addEventListener('click', function () {
            if (parent) {                
                var parentdialog = document.querySelector(parent);
                if (parentdialog) {
                    parentdialog.close();
                }
            }
            dialog.showModal();
        });

        var dialogBtn = dialog.querySelector('.close');
        if (dialogBtn) {
            dialogBtn.addEventListener('click', function () {
                dialog.close();
            });
        }

    } else if (actionBtnId.isClassSelector()) {

        var btns = document.querySelectorAll(actionBtnId);
        var dialog = document.querySelector(dialogId);

        Array.from(btns).forEach((element) => {
            element.addEventListener('click', function () {
               
                dialog.showModal();
            });
        });
        dialog.querySelector('.close').addEventListener('click', function () {
            dialog.close();
        });
    }

   

}


function configureToggle(toggleId, callback) {
    var input = document.querySelector(toggleId);
    if (input) {
        input.addEventListener("change", callback)
    }
}
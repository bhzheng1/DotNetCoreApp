// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var callAjax = function (url, type, ajaxData, useLoading, cbParameters, cb) {
    $.ajax({
        url: url,
        data: ajaxData,
        beforeSend: function () {
            if (useLoading) {
                loading(true);
            };
        },
        success: function (dataset) {
            if (cbParameters) {
                cb(cbParameters, dataset);
            }
            else {
                cb(dataset);
            }
        },
        error: function (e) {
            if (useLoading) {
                loading(false);
            };
            alert(e);
        },
        complete: function () {
            if (useLoading) {
                loading(false);
            };
        },
    });
};

document.addEventListener('DOMContentLoaded', function () {
    // your code here
    setTimeout(loading(false), 500);
}, false);

var currencyFormatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
});

function isNumeric(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function delay() {
    var timer = 0;
    return function (obj, callback, ms) {
        sender = obj;
        clearTimeout(timer);
        timer = setTimeout(callback, ms || 0);
    };
}

var delayFunction = delay();

function updateTotal(sender) {
    //console.log('Time elapsed!', sender.value);
    //get the attribute value
    var lastAmt = sender.getAttribute('value') ? parseInt(sender.getAttribute('value').replace(/\$|,/g, "")) : 0;

    //get the property value
    var currentAmt = sender.value ? parseInt(sender.value.replace(/\$|,/g, "")) : 0;
    var totalAmt = document.getElementById('total').getAttribute('value') ? parseInt(document.getElementById('total').getAttribute('value')) : 0;

    if (lastAmt != currentAmt) {
        //set the attribute value
        sender.setAttribute('value', currentAmt.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','));

        //set the property value
        sender.value = currentAmt.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');

        //利用js原生的selector设置dom的property
        document.getElementById('total').value = totalAmt - lastAmt + currentAmt;

        //利用js原生的selector设置dom的attribute
        document.getElementById('total').setAttribute('value', totalAmt - lastAmt + currentAmt);

        document.getElementById('total').textContent = currencyFormatter.format(totalAmt - lastAmt + currentAmt);
    }
}

function syncTotal(sender) {
    var lastAmt = sender.getAttribute('value') ? parseInt(sender.getAttribute('value').replace(/\$|,/g, "")) : 0;
    var currentAmt = sender.value ? parseInt(sender.value.replace(/\$|,/g, "")) : 0;

    if (lastAmt != currentAmt) {
        $("#input").text(function (n, currentContent) {
            return currencyFormatter.format(parseInt(currentContent.replace(/\$|,/g, "")) + currentAmt - lastAmt);
        });

        sender.setAttribute('value', currentAmt.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','));
        sender.value = currentAmt.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    }
}

var loading = function (isLoading) {
    var spinner = document.getElementById("loading");
    if (!spinner) {
        var over = document.createElement('div');
        over.setAttribute('id', 'loading');
        over.setAttribute('class', 'loading');
        over.appendChild(document.createTextNode('Loading&#8230;'));
        var header = document.getElementsByTagName("header")[0];
        insertAfter(over, header);
        spinner = document.getElementById("loading");
    };
    if (isLoading) {
        spinner.setAttribute("hidden", false);
    } else {
        spinner.setAttribute("hidden", true);
    }
}

// create function, it expects 2 values.
var insertAfter = function (newElement, targetElement) {
    // target is what you want it to go after. Look for this elements parent.
    var parent = targetElement.parentNode;

    // if the parents lastchild is the targetElement...
    if (parent.lastChild == targetElement) {
        // add the newElement after the target element.
        parent.appendChild(newElement);
    } else {
        // else the target has siblings, insert the new element between the target and it's next sibling.
        parent.insertBefore(newElement, targetElement.nextSibling);
    }
}
//insert new Element after some reference Element
function insertAfter(newElement, referenceElement) {
    referenceElement.parentNode.insertBefore(newElement, referenceElement.nextSibling);
}


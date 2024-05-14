// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function NextPage() {
    let val = document.querySelector("#pageNumber").value;
    if (val != "5") {
        if (val == 1) {
            window.location.href = '/SecondPage/Index';
        } else if (val == 2) {
            window.location.href = '/ThirdPage/Index';
        } else if (val == 3) {
            window.location.href = '/FourthPage/Index';
        } else if (val == 4) {
            window.location.href = '/FifthPage/Index';
        }
    }

    console.log(val);
}

function PreviousPage(){
    let val = document.querySelector("#pageNumber").value;
    if (val != "1") {
        if (val == 5) {
            window.location.href = '/FourthPage/Index';
        } else if (val == 4) {
            window.location.href = '/ThirdPage/Index';
        } else if (val == 3) {
            window.location.href = '/SecondPage/Index';
        } else if (val == 2) {
            window.location.href = '/FirstPage/Index';
        }
    } 
    console.log(val);
}

function GoToPage() {
    let val = document.querySelector("#pageNumber").value;
    if (val == 5) {
        window.location.href = '/FifthPage/Index';
    } else if (val == 4) {
        window.location.href = '/FourthPage/Index';
    } else if (val == 3) {
        window.location.href = '/ThirdPage/Index';
    } else if (val == 2) {
        window.location.href = '/SecondPage/Index';
    } else if (val == 1) {
        window.location.href = '/FirstPage/Index';
    }
}

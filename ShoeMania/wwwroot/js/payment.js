window.onload = function () {

    const name = document.getElementById('name');
    const cardnumber = document.getElementById('cardnumber');
    const expirationdate = document.getElementById('expirationdate');
    const securitycode = document.getElementById('securitycode');
    const output = document.getElementById('output');
    const ccicon = document.getElementById('ccicon');
    const ccsingle = document.getElementById('ccsingle');
    const generatecard = document.getElementById('generatecard');


    let cctype = null;


    document.querySelector('.preload').classList.remove('preload');
    document.querySelector('.creditcard').addEventListener('click', function () {
        if (this.classList.contains('flipped')) {
            this.classList.remove('flipped');
        } else {
            this.classList.add('flipped');
        }
    })

    name.addEventListener('input', function () {
        if (name.value.length == 0) {
            document.getElementById('svgname').innerHTML = 'John Doe';
            document.getElementById('svgnameback').innerHTML = 'John Doe';
        } else {
            document.getElementById('svgname').innerHTML = this.value;
            document.getElementById('svgnameback').innerHTML = this.value;
        }
    });

    document.getElementById('cardnumber').addEventListener('input', function (e) {

        var input = e.target.value.replace(/\D/g, '').substring(0, 16);
        var formattedInput = input.replace(/(\d{4})(?=\d)/g, '$1 ');
        document.getElementById('svgnumber').innerHTML = formattedInput;
        document.getElementById('cardnumber').value = formattedInput;

    });

    expirationdate.addEventListener('input', function () {
        if (expirationdate.value.length == 0) {
            document.getElementById('svgexpire').innerHTML = "01/23"
        } else {
            document.getElementById('svgexpire').innerHTML = this.value;
        }
    })

    securitycode.addEventListener('input', function () {
        document.querySelector('.creditcard').classList.add('flipped');

        if (securitycode.value.length == 0) {
            document.getElementById('svgsecurity').innerHTML = "981"
        } else {
            document.getElementById('svgsecurity').innerHTML = this.value;
        }
    })


    name.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    cardnumber.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    expirationdate.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    securitycode.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.add('flipped');
    });
};
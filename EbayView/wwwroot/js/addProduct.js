//let user = JSON.parse(sessionStorage.user || null);
//let loader = document.querySelector('.loader');

// checknig user is logged in or not
//window.onload = () => {
//    if (user) {
//        if (!compareToken(user.authToken, user.email)) {
//            location.replace('/login');
//        }
//    } else {
//        location.replace('/login');
//    }
//}
// price inputs
//const actualPrice = document.querySelector('#actual-price');
//const discountPercentage = document.querySelector('#discount');
//const sellingPrice = document.querySelector('#sell-price');
//discountPercentage.addEventListener('input', () => {
//    if (discountPercentage.value > 100) {
//        discountPercentage.value = 90;
//    } else {
//        let discount = actualPrice.value * discountPercentage.value / 100;
//        sellingPrice.value = actualPrice.value - discount;
//    }
//})
//sellingPrice.addEventListener('input', () => {
//    let discount = (sellingPrice.value / actualPrice.value) * 100;
//    discountPercentage.value = discount;
//})

// upload image handle
let uploadImages = document.querySelectorAll('.fileupload');
let imagePaths = []; // will store all uploaded images paths;
uploadImages.forEach((fileupload, index) => {
    fileupload.addEventListener('change', () => {
        const file = fileupload.files[0];
        let imageUrl; 
        if (file.type.includes('image')) {
            if (file.type.includes('image')) {
                // means user uploaded an image
                //fetch('/s3url').then(res => res.json())
                //    .then(url => {
                //        fetch(url, {
                //            method: 'PUT',
                //            headers: new Headers({ 'Content-Type': 'multipart/form-data' }),
                //            body: file
                //        }).then(res => {
                //            imageUrl = url.split("?")[0];
                //            imagePaths[index] = imageUrl;
                //            let label = document.querySelector(`label[for=${fileupload.id}]`);
                //            label.style.backgroundImage = `url(${imageUrl})`;
                //            let productImage = document.querySelector('.product-image');
                //            productImage.style.backgroundImage = `url(${imageUrl})`;
                //        })
                //    })
            }

        } else {
            showAlert('upload image only');
        }
    })
})
// generate image upload link
  function generateUrl() {
    let date = new Date();
    let id = parseInt(Math.random() * 10000000000); 
    const imageName = `${id}${date.getTime()}.jpg`; 
      return imageName;
}

// form submission 
const productName = document.querySelector('#product-name'); 
const des = document.querySelector('#des'); 
const stock = document.querySelector('#stock');
const tags = document.querySelector('#catg'); 
const addProductBtn = document.querySelector('#add-btn');
const validateForm = () => {
    if (!productName.value.length) {
        return showAlert('enter product name');
    } else if (!des.value.length) {
        return showAlert('enter detail description about the product');
    } else if (!imagePaths.length) { // image link array
        return showAlert('upload atleast one product image') 
    } else if (!actualPrice.value.length) {
        return showAlert('you must add pricings');
    } else if (stock.value < 20) {
        return showAlert('you should have at least 20 items in stock');
    } else if (!tags.value.length) {
        return showAlert('enter few tags to help ranking your product in search');
    }  
    return true;
}

addProductBtn.addEventListener('click', () => {
    storeSizes();
    // validate form
    if (validateForm()) { // validateForm return true or false while doing validation

    }
})

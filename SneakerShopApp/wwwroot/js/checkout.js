document.addEventListener("DOMContentLoaded", function (event) {

    const API_ROOT = "https://merchants.api.sandbox-utrust.com/api";
    const API_KEY = "u_test_api_7dee60ad-e518-411f-a22a-5c1ec437f1cc";
    const url = "https://" + window.location.host;

    $.ajax({
        url: url + "/Shop/GetCartProducts",
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            var total = 0;
            var customer = res[0].customer;
            var items = [];
            for (var id in res) {
                total += +res[id].price
                items.push({ name: res[id].description, price: res[id].price, quantity:1, currency :"RON"});
            }
            orderParams = {
                data: {
                    type: "orders",
                    attributes: {
                        order: {
                            reference: "order-51367",
                            amount: {
                                total: "" + total,
                                currency: "RON",
                            },
                            return_urls: {
                                return_url: url + "/Shop/OrderConfirmation",
                            },
                            line_items: items,
                        },
                        customer: {
                            email: customer,
                            country: "RO",
                        },
                    },
                },
            };

            // utrust api
            const utrustApi = {
                createOrder: (params, token) =>
                    fetch(API_ROOT + "/stores/orders/", {
                        method: "POST",
                        headers: {
                            "content-type": "application/vnd.api+json",
                            authorization: "Bearer " + API_KEY,
                        },
                        body: JSON.stringify(params),
                    }).then((response) => response.json()),
            };
            $("#utrust-checkout").click(function () {
                utrustApi.createOrder(orderParams).then((response) => {
                    window.location.replace(response.data.attributes.redirect_url);
                });
            });
        }
    });
});
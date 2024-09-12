var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
connection.on("LoadAccount", function () {
    $.get('/YourRazorPage/GetSessionValue', function (data) {
        try {
            var acc = JSON.parse(data);
            if (acc && acc.AccountID && acc.UserName
                && acc.Password && acc.FullName && acc.Type) {
                location.href = '/AccountView/Index';
            } else {
                location.href = '/Home/Home';
            }
        } catch (error) {
            console.error('Error parsing JSON:', error);
            location.href = '/Home/Home';
        }

    });
});
connection.on("LoadCategory", function () {
    location.href = '/CategoryView'
});
connection.on("LoadSupplier", function () {
    location.href = '/SupplierView'
});
connection.on("LoadProduct", function () {
    location.href = '/ProductView'
});
connection.on("LoadOrder", function () {
    location.href = '/OrderView'
});
connection.on("LoadCustomer", function () {
    location.href = '/CustomerView'
});
connection.on("LoadOrder_Detail", function () {
    location.href = '/Order_DetailView'
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});
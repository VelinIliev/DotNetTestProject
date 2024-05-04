var dataTable;
function tableLoad(status) {
    dataTable = $('#tblData').DataTable( {
        ajax: `/admin/order/getall?status=${status}`,
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group-sm mx-0" role="group">
                            <a href="/admin/order/details?orderId=${data}" class="btn mx-0 p-0"><i class="bi bi-pencil-square"></i>Edit</a>
                        </div>`
                },
                "width": "5%"
            },
        ]
    } );
}

document.addEventListener("DOMContentLoaded", ()=> {
    var url = window.location.search;
    if (url.includes("inproccess")) {
        tableLoad("inproccess");
    } else if (url.includes("completed")) {
        tableLoad("completed");
    } else if (url.includes("approved")) {
        tableLoad("approved");
    } else if (url.includes("pending")) {
        tableLoad("pending");
    } else {
        tableLoad("all");
    }
});

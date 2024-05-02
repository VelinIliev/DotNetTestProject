var dataTable;
function tableLoad() {
    dataTable = $('#tblData').DataTable( {
        ajax: '/admin/company/getall',
        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'streetAddress', "width": "25%" },
            { data: 'city', "width": "20%" },
            { data: 'state', "width": "5%" },
            { data: 'phoneNumber', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group-sm mx-0" role="group">
                            <a href="/admin/company/upsert?id=${data}" class="btn mx-0 p-0"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a onclick=Delete("/admin/company/delete?id=${data}") class="btn text-danger mx-0 p-0"><i class="bi bi-trash-fill"></i> Delete</a>
                        </div>`
                },
                "width": "15%"
            },
        ]
    } );
};

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

document.addEventListener("DOMContentLoaded", tableLoad);

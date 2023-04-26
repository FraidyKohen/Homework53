$(() => {
    const modal = new bootstrap.Modal($('#add-modal')[0]);
    let mode = '';
    let id = '';

    function updateTable() {
        $('tbody').empty();
        $.get('/home/getPeople', function (people) {
            people.forEach(function (person) {
                let id = person.id;
                $('tbody').append(`<tr>
<td>${person.firstName}</td>
<td>${person.lastName}</td>
<td>${person.age}</td>
<td>
    <button class="btn btn-warning" id="edit" data-id="${id}">Edit</button>
</td>
<td>
    <button class="btn btn-danger" id="delete" data-id="${id}">Delete</button>
</td>
</tr>`)
            });
        });
    }


    updateTable();

    $('#add-person').on('click', function () {
        modal.show();
        mode = 'add';
        console.log(mode);
    });



    $('table').on('click', '#edit', function () {
        id = $(this).attr('data-id');
        modal.show();
        mode = 'edit';
        console.log(mode);
        $.get(`/home/getPersonById?id=${id}`, function (person) {
            console.log(person);
            $('#first-name').val(person.firstName);
            $('#last-name').val(person.lastName);
            $('#age').val(person.age);
        })
    });

    $('#save-person').on('click', function () {
        const firstName = $('#first-name').val();
        const lastName = $('#last-name').val();
        const age = $('#age').val();

        if (mode === 'add') {
            $.post('/home/addperson', { firstName, lastName, age }, function () {
                modal.hide();
                updateTable();
            });
        }
        else if (mode === 'edit') {
            //console.log(id);
            //$.post('/home/editPerson', { firstName, lastName, age, id }, function () {
            //    modal.hide();
            //    updateTable();

            //    $('#first-name').val('');
            //    $('#last-name').val('');
            //    $('#age').val('');
            //});
            $.post(`/home/deletePerson?id=${id}`, function () {
                $.post('/home/addperson', { firstName, lastName, age }, function () {
                    modal.hide();
                    updateTable();
                });
            })
        }
    });

    $('table').on('click', '#delete', function () {
        const id = $(this).attr('data-id');
        $.post(`/home/deletePerson?id=${id}`, function () {
            updateTable();
        })
    })

});

let employees = JSON.parse(localStorage.getItem("employees")) || [];
let editIndex = -1;
function addEmployee()
{
    let id = document.getElementById("empId").value;
    let name = document.getElementById("empName").value;
    let dept = document.getElementById("department").value;
    let salary = document.getElementById("salary").value;

    if(id === "" || name === "" || dept === "" || salary === "")
{
    alert("Please fill all fields");
    return;
}

   if (editIndex === -1)
{
    employees.push({
        id: id,
        name: name,
        department: dept,
        salary: salary
    });
}
else
{
    employees[editIndex] = {
        id: id,
        name: name,
        department: dept,
        salary: salary
    };

    editIndex = -1;
}
    saveToLocalStorage();
    displayEmployees();

    document.getElementById("empId").value = "";
    document.getElementById("empName").value = "";
    document.getElementById("department").value = "";
    document.getElementById("salary").value = "";
}
function editEmployee(index)
{
    document.getElementById("empId").value =
        employees[index].id;

    document.getElementById("empName").value =
        employees[index].name;

    document.getElementById("department").value =
        employees[index].department;

    document.getElementById("salary").value =
        employees[index].salary;

    editIndex = index;
}
function deleteEmployee(index)
{
    employees.splice(index, 1);

    saveToLocalStorage();
    displayEmployees();
}
function saveToLocalStorage()
{
    localStorage.setItem("employees", JSON.stringify(employees));
}
function displayEmployees()
{
    let table = document.getElementById("employeeTable");

    table.innerHTML = "";

    employees.forEach((emp, index) =>
    {
        let row = table.insertRow();

        row.insertCell(0).innerHTML = emp.id;
        row.insertCell(1).innerHTML = emp.name;
        row.insertCell(2).innerHTML = emp.department;
        row.insertCell(3).innerHTML = emp.salary;

        row.insertCell(4).innerHTML =
        `<button class="edit-btn"
onclick="editEmployee(${index})">Edit</button>

<button class="delete-btn"
onclick="deleteEmployee(${index})">Delete</button>`;
    });
    updateEmployeeCount();
}

displayEmployees();
function searchEmployee()
{
    let input =
    document.getElementById("searchBox").value.toLowerCase();

    let rows =
    document.getElementById("employeeTable").getElementsByTagName("tr");

    for(let i = 0; i < rows.length; i++)
    {
        let name =
        rows[i].cells[1].textContent.toLowerCase();

        if(name.includes(input))
        {
            rows[i].style.display = "";
        }
        else
        {
            rows[i].style.display = "none";
        }
    }
}
updateEmployeeCount();
updateSalaryTotal();
function updateEmployeeCount()
{
    document.getElementById("employeeCount").innerHTML =
    "Total Employees: " + employees.length;
}
function updateSalaryTotal()
{
    let total = 0;

    employees.forEach(emp =>
    {
        total += Number(emp.salary);
    });

    document.getElementById("salaryTotal").innerHTML =
    "Total Salary: " + total;
}
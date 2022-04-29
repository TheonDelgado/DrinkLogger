function calculate() 
{
    const table = document.getElementById("records");
    const resultArea = document.getElementById("result");

    let result = 0;

    for(var i = 1; i < table.rows.length ; i++)
    {
        result = result + +table.rows[i].cells[i].innerHTML;
    }

    resultArea.append(`${result}`);
}
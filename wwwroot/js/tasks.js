const changedTasks = {};

function addTask(taskId) {
    if (changedTasks[taskId] == undefined) {
        changedTasks[taskId] = document.getElementById(taskId).checked;
    } else {
        delete changedTasks[taskId];
    }

    document.getElementById("update-task").hidden = Object.entries(changedTasks).length <= 0;

    console.log(changedTasks);
}
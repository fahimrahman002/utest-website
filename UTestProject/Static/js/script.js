let ques_count = 0;
function showQuestions(index) {
  const ques_text = document.querySelector(".ques-text");
  const option_list = document.querySelector(".ques-option");
  const length = 4;
  let ques_tag =
    "<h5 class='p-2 ques-text'>" +
    questions[index].number +
    "." +
    questions[index].question +
    "</h5>";

  try {
    for (var i = 0; i < length; i++) {
      option_list.innerHTML +=
        '<div class="col-md-5 option  m-2" ><h6 class="p-3">' +
        questions[index].options[i] +
        "</h6></div>";
    }
    ques_text.innerHTML = ques_tag;
    //   option_list.innerHTML = option_tag;

    const option = option_list.querySelectorAll(".option");
    for (let i = 0; i < option.length; i++) {
      option[i].setAttribute("onclick", "optionSelected(this)");
    }
  } catch (e) {}
}
showQuestions(0);
function optionSelected(answer) {
  let userAns = answer.textContent;
  let correctAns = questions[ques_count].answer;
  console.log(userAns);
  console.log(correctAns);
  if (userAns == correctAns) {
    answer.classList.add("correct");
    console.log("answer is correct");
  } else {
    answer.classList.add("inCorrect");
    console.log("answer is wrong");
  }
}

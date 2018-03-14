#load "./build/BuildParameters.cake"

/* ---------------------------------------------------------------------------------------------------- */
/* Arguments */

var parameters = BuildParameters.GetInstance(Context);

/* ---------------------------------------------------------------------------------------------------- */
/* Tasks Targets */

Task("Default")
    .Does(() =>
    {
        parameters.ShowInfo();
    });

/* ---------------------------------------------------------------------------------------------------- */
/* Execution */

RunTarget(parameters.Target);
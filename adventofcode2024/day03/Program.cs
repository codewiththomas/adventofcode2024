using System.Text.RegularExpressions;

var code = File.ReadAllText("input.txt");

string[] ExtractInstructions(string code)
{
    // ONLY mul(xxxx,yyyy) is valid, with xxxx and yyyy being integers
    // mul(1,2)              => valid
    // xmul(1,2) => mul(1,2) => valid
    // mul(1,2)x => mul(1,2) => valid
    // mul(1,2]              => invalid
    // mul(1,2,3)            => invalid
    // mul( 1 , 2 )          => invalid
    // mul 1,2)              => invalid

    // Commands "do()" and "don't()" are valid commands as well

    // The input "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))" should return
    // ["mul(2,4)", "mul(5,5)", "mul(11,8)", "mul(8,5)"]

    var regex = new Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)");
    var matches = regex.Matches(code);

    var instructions = new List<string>();
    foreach (Match match in matches)
    {
        Console.WriteLine(match.Value);
        instructions.Add(match.Value);
    }
    return instructions.ToArray();
}


string[] FilterExecutableInstructions(ICollection<string> instructions)
{
    bool isEnabled = true;
    var validInstructions = new List<string>();
    foreach (var instruction in instructions)
    {
        if (instruction.Equals("do()"))
        {
            isEnabled = true;
            continue;
        }
        else if (instruction.Equals("don't()"))
        {
            isEnabled = false;
            continue;
        }

        if (isEnabled)
        {
            validInstructions.Add(instruction);
        }
    }
    return validInstructions.ToArray();
}





int ExecuteInstruction(string instruction)
{
    string tempInstruction = instruction;
    tempInstruction = tempInstruction.Replace("mul(", "");
    tempInstruction = tempInstruction.Replace(")", "");
    var values = tempInstruction.Split(",");
    return int.Parse(values[0]) * int.Parse(values[1]);
}


var instructions = ExtractInstructions(code);
var executableInstructions = FilterExecutableInstructions(instructions);

int sum = 0;
foreach (var instruction in executableInstructions)
{
    int product = ExecuteInstruction(instruction);
    sum += product;
}

Console.WriteLine(sum);
TASK_PATH=$1
TASK_NAME=$2
TASK_TYPE=$3

if [[ -z $TASK_TYPE ]] ; then 
    TASK_TYPE=console
fi

mkdir $TASK_PATH
cd $TASK_PATH

mkdir $TASK_NAME
cd $TASK_NAME
dotnet new $TASK_TYPE
dotnet add $TASK_NAME.csproj package StyleCop.Analyzers
dotnet new 
cd ..

mkdir $TASK_NAME.tests
cd $TASK_NAME.tests
dotnet new nunit
dotnet add $TASK_NAME.tests.csproj reference ../$TASK_NAME/$TASK_NAME.csproj
cd ..

dotnet new sln
dotnet sln add *.sln $TASK_NAME/$TASK_NAME.csproj
dotnet sln add *.sln $TASK_NAME.tests/$TASK_NAME.tests.csproj

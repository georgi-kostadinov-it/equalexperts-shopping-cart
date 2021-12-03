# Equal Experts - Shopping Cart Problem

Submission reference number: ````5b8d0fd276b6d288905ed2f63a934e057e8feca2````.

This project implements a simple Shopping Cart using .net core.

We assume that you have already unpacked the ````step<N>.zip```` file in a folder on your computer. The folder contains a ````vagrantfile```` for compiling linux-based .net core-enabled VM. 

Please, refer to the prerequisites below describing what's needed in order to run the application in an isolated environment.

## <a name="prerequisites"></a> Prerequisites

- [Virtual Box](https://www.virtualbox.org/)
- [Vagrant](https://www.vagrantup.com/downloads.html)

## <a name="buildingvm"></a> Building VM

Open a command shell, navigate to the folder containing the ````vagrantfile````, and do:
````
vagrant up
````

## <a name="usingvm"></a> Using VM

Once the VM has been created, connect to it and do:
````
vagrant ssh
````

You'll see a command prompt similar to this:
````
vagrant@dotnetcore:~$
````

## <a name="buildingapp"></a> Building the Shopping Cart application

To build the .net core application, at the command prompt, do:

````
cd /home/vagrant/src
dotnet build
````

## <a name="bruntestsuite"></a> Running the Shopping Cart test suite

To run application's test suite, at the command prompt, do:

````
cd /home/vagrant/src
dotnet test
````

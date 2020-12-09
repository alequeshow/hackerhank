# Counting Valleys

An avid hiker keeps meticulous records of their hikes. During the last hike that took exactly _**steps**_ steps, for every step it was noted if it was an uphill, _**U**_, or a downhill, _**D**_ step. Hikes always start and end at sea level, and each step up or down represents a _**1**_ unit change in altitude. We define the following terms:

* A mountain is a sequence of consecutive steps above sea level, starting with a step up from sea level and ending with a step down to sea level.
* A valley is a sequence of consecutive steps below sea level, starting with a step down from sea level and ending with a step up to sea level.

Given the sequence of up and down steps during a hike, find and print the number of valleys walked through. 

### Example

> steps = 8 path = [DDUUUDD]

The hiker first enters a valley _**2**_ units deep. Then they climb out and up onto a mountain _**2**_ units high. Finally, the hiker returns to sea level and ends the hike.

### Function Description

Complete the countingValleys function in the editor below.

countingValleys has the following parameter(s):

* int steps: the number of steps on the hike
* string path: a string describing the path

### Returns

* int: the number of valleys traversed

### Input Format

The first line contains an integer _**steps**_, the number of steps in the hike.
The second line contains a single string _**path**_, of _**steps**_ characters that describe the path.

### Constraints

* 2 <= steps <= 10 power (6)
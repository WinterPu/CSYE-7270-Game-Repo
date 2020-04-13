# CSYE 7270 Assignment 05

## 1. Introduction

### Assignment Topic Selection

Game AI

### Artificial Intelligence Technique Selection

Artificial Intelligence Agents

###Method

Reinforcement Learning

### Version

```
 Version information:
  ml-agents: 0.15.1,
  ml-agents-envs: 0.15.1,
  Communicator API: 0.15.0,
  TensorFlow: 2.0.1
```



## 2. Model

### Observation

9 variables including

* Player's local Position: x, y, z
* Target's local Position: x, y, z
* Player's velocity: x, y, z

### Action

3 variables including:

* Horizontal Input
* Vertical Input
* Jump Input



## 3. Game

###Game Introduction 

This is a simple platform game. Control your red cube to hit the yellow target cube.

### Game Control

* `W` Forward
* `S` Backward
* `A` Left
* `D`Right
* `Space` Jump



##4. Result & Quality

```
Mean Reward: 0.995 / 1.0
```

![Capture](C:\Users\Winter Pu\Desktop\CSYE 7270 Assignment 05 Report\images\Capture.PNG)



## 5. Test

```
Total: 1000 tests, it hits 991
Total Accuracy = 99.1%
```



## 6. Video

https://youtu.be/Va5FODdmpGw



## 7. Reference

* [MLAgents Document 0.15.1]  https://github.com/Unity-Technologies/ml-agents/blob/0.15.1/docs/Readme.md
* [Making a New Learning Environment] https://github.com/Unity-Technologies/ml-agents/blob/0.15.1/docs/Learning-Environment-Create-New.md
* [Design Agent] https://github.com/Unity-Technologies/ml-agents/blob/0.15.1/docs/Learning-Environment-Design-Agents.md
* [MLAgents Example] https://github.com/Unity-Technologies/ml-agents/tree/0.15.1/Project/Assets/ML-Agents/Examples



## 8. License

* **Unity License**

  Personal Use

  **Eligibility:** Revenue or funding less than $100K in the last 12 months

* **Unity ML-Agents License**

  Apache-2.0

  https://github.com/Unity-Technologies/ml-agents/blob/0.15.1/LICENSE

* **Video Background Music**

  * Confederation Line

    Confederation Line by Admiral Bob (c) copyright 2020 Licensed under a Creative Commons Attribution (3.0) license. http://dig.ccmixter.org/files/admiralbob77/60909 Ft: geoffpeters


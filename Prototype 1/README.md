# Prototype 1 - Horizontal Unity Prototype

FigmaXR: Immersive Prototype Testing in Virtual Environments
Academic Project - Interactive Prototype 1 (IP1)
Course: XR Prototyping and Design
Assessment: Interactive Prototype 1 - Unity Horizontal Prototype
Submission: Week 5 Studio Class Testing Session

Project Overview

FigmaXR is a horizontal prototype that explores immersive UI/UX testing in virtual environments. This Unity-based prototype demonstrates how environmental contexts affect interface usability and user attention during design evaluation processes.
Three-Sentence Pitch: This project is traditional UI/UX testing for mobile applications but using Unity 3D for immersive environment simulation and device interaction. Users can test Figma prototypes in different realistic environments (office vs café) while holding virtual devices (iPhone, iPad, laptop) in first-person view. The system allows designers to understand how environmental factors like lighting and ambient noise affect user interface readability and user focus.

IP1 Testing Objectives

Primary Research Question
"Does environmental context significantly impact user attention and interface evaluation during UI testing?"
Testing Goals

Evaluate whether different environments (quiet office vs noisy café) affect user focus on mobile interfaces
Assess if first-person device interaction provides more realistic testing conditions compared to traditional methods
Gather user feedback on the concept's potential value for design workflows
Identify key areas for IP2 vertical prototype development

Prototype Features (Horizontal Implementation)
Core Functionality - "Appears Complete"

First-Person Navigation: WASD movement with mouse look controls
Two Contrasting Environments:

Office Environment: Quiet, cold lighting, professional atmosphere
☕ Café Environment: Warm lighting, background noise, social atmosphere

Three Device Types: iPhone, iPad, and Laptop with realistic proportions
FPS-Style Device Interaction: Pick up and hold devices in first-person view
Dynamic Lighting Control: Real-time adjustment of environment lighting intensity
Figma Interface Integration: Display and navigate through prototype screens

Interactive Elements

Environment switching via UI button
Device pickup system with smooth animations
Screen interaction for interface navigation
Lighting intensity slider with real-time feedback
Device return system (Press R to return devices to desk)

🛠️ Technical Implementation
Development Tools

Unity 3D (2022.3 or later) - Primary development platform
C# Programming - All interaction logic and systems
TextMeshPro - UI text rendering
Character Controller - Physics-based movement

AI and External Sources Used

Claude AI (Anthropic) - Code structure guidance, script development assistance, and debugging support
Unity Documentation - Official API references and best practices
Stack Overflow - Specific technical problem solutions
YouTube Unity Tutorials - First-person controller implementation concepts

Full reference list provided in Statement of Originality

IP1 Testing Protocol
Testing Methodology
Comparative User Testing with Observational Research
Session Structure (5 minutes per participant)

Introduction and Setup (30 seconds)

Explain project concept and basic controls
Brief on environmental context testing purpose


Familiarization (60 seconds)

Practice WASD movement and mouse look
Learn device pickup (click iPhone/iPad/Laptop)
Test screen interaction and interface navigation


Office Environment Testing (90 seconds)

Pick up and test interfaces on different devices
Observe user focus and attention in quiet setting
Note interface readability and interaction patterns


Environment Transition (30 seconds)

User clicks environment switch button
Experience lighting and audio changes
Observe immediate reactions to context shift


Café Environment Testing (90 seconds)

Continue testing same interfaces with background noise
Compare user attention and focus levels
Test light adjustment slider functionality


Immediate Feedback Collection (60 seconds)

Quick verbal feedback session
4-5 question survey on concept value and usability



Data Collection Methods

Observational Notes: User behavior changes between environments
Task Completion Timing: Speed of interaction in different contexts
Verbal Feedback: Immediate reactions and concept validation
Quick Survey: Structured feedback on environmental impact and tool value

Success Metrics for IP1

80% of users can operate first-person controls within 30 seconds
70% of users notice environmental differences affecting their focus
60% of users perceive value in environment-based UI testing
Collection of actionable feedback for IP2 development direction

User Controls
Navigation

WASD - Move around the environment
Mouse - Look around (first-person view)
Right Click + Drag - Alternative camera control
ESC - Toggle cursor lock

Interaction

Left Click - Interact with objects (pickup devices, click UI)
Left Click - Navigate Figma interfaces on device screens
R Key - Return held device to desk

UI Controls

Switch Environment Button - Toggle between Office and Café
Light Intensity Slider - Adjust environmental lighting
Device Status Display - Shows current held device information

Setup Instructions for Testing
Prerequisites

Unity 3D (2022.3 or later)
Desktop/Laptop with mouse and keyboard
Audio output (speakers/headphones) for environmental sounds

Pre-Class Setup

Load Unity project and IP1_Scene
Verify all device interactions work correctly
Test environment switching and lighting controls
Prepare 2-3 Figma interface images in devices array
Setup observation sheets and feedback forms

Class Testing Setup

Position computer for easy participant access
Prepare timer for 5-minute sessions
Have backup controls instruction sheet ready
Setup data collection materials (observation forms, questionnaires)

AI & Sources Disclosure (Short)

Text drafting: ChatGPT (GPT-5 Thinking) was used to draft the testing plan structure and IUQ-12 items. I edited and validated all text for fit with the prototype and course requirements.

Code support: Claude was used to suggest small Unity C# snippets (e.g., raycast-based pickup & interaction checks, binding a UI Slider to lighting intensity and simple environment-switch state handling). I integrated, refactored, and tested all code; final behaviour and fixes are my own.

Method guidance: Methods and deliverables follow DECO2300/7230 guidance on testing sessions, scripted tasks, think-aloud, and questionnaires.

Mobile phone UI size illustration (JPG) from: Pixso, “手机界面设计尺寸多大？大神最新总结！” (Design Skills). Accessed 29 Aug 2025. Copyright © Pixso / Shenzhen Bosiyunchuang Technology Co., Ltd. Used for educational, non-commercial prototype demonstration.

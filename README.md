# SoftGamesTest


🔹 Project 1: Deck Manager ("Ace of Shadows")

Description:

A dynamic card deck visualizer that generates 144 cards stacked vertically. The top card moves to another stack every second, with a visual counter and smooth animation. Switching back to this tab resets the entire deck.

Features:

  - 144 cards generated with offset stacking

  - Smooth animated card transfer with flip effect

  - Visual counters for both stacks

  - Reset and rebuild when tab is selected

  - Two buttons to increase or decrease card move speed

Controls:

  - Speed+: Decreases the interval between card moves (faster)

  - Speed-: Increases the interval (slower)

  - Speed range: 0.1s to 2.0s

Code Highlights:

 - Namespace: AceOfShadows

Classes:

 - DeckManager: Controls logic and card movement

 - StackController: Manages stacks and counters

 - CardController: Animates individual cards




🔥 Project 2: Fire Color Animator ("Phoenix Flame")

Description:

A looping fire particle effect whose color changes (orange → green → blue) when a button is pressed. Color change is animated through the Animator system, and the button color updates to preview the next fire color.

Features:

 - Fire particle system with animated color

 - Button to trigger next color transition

 - Button background updates to preview the next color

 - Smooth blending with Animator CrossFade()

Color Sequence:

 - Orange (#FF6E00)

 - Green

 - Blue

 - Back to Orange (loop)

Code Highlights:

 - Namespace: PhoenixFlame

Classes:

 - FireColorController: Controls transitions and button UI



🎨 Project 3: Dialogue System ("Magic Words")

Description:

Loads JSON dialogue and avatar data from a remote endpoint, parses the content, and displays it in a scrollable UI. Unicode emoji placeholders (e.g. {laughing}) are replaced with their corresponding emoji.

Features:

 - Remote JSON data loading (dialogue + avatars)

 - Emoji placeholders converted to Unicode

 - Avatar images dynamically downloaded (with fallback)

 - Right/Left alternating message layout

 - Fully scrollable conversation view

Layout Logic:

 - Dialogues are displayed in sequence

 - Avatar/message alignment alternates automatically (right, left, right...)

 - Each avatar is matched with position (right/left); if missing or invalid, a placeholder is shown

Code Highlights:

 - Namespace: MagicWords

Classes:

 - DialogueManager: Loads and parses data

 - DialogueEntryUI: Displays a single message entry

 - EmojiDictionary: Maps {name} to Unicode emojis




⚖️ Tab System (Common UI)

Description:

A simple tab-switching system that enables/disables specific objects based on which of the three projects is selected.

Features:

 - Tabs: All clickable tab buttons

 - ActiveTabs: Visual highlight for the current tab

 - Uses lists to enable/disable relevant GameObjects per project

 - Automatically resets Project 1 when its tab is selected

Code Highlights:

 - Class: TabManager

 - Method: ShowProject(int index)

 - Index: 1 (Deck), 2 (Fire), 3 (Dialogue)

# helpful Unity Extensions

This repository primarily exists as a way for me to collect and share some helpful tools I use when working with Unity. 
This project is constantly a work in progress, so use with care.

## Install
Via Package Manager:
```
https://github.com/oneir0mancer/unity-extensions.git
```

## Extensions

### CollectionsExtensions
Helper methods for working with collections: getting Min/Max/Random element, or shuffling collection inplace,
or printing all elements of a collection to string.

### CoroutineExtensions
Coroutines that extend WaitForSeconds with the ability to pause, or interupt if condition is met, or invoke callback every frame.
And an extension method that uses lambda functions to start simple coroutines.

### MathExtensions
Some math operations, like Modulo and Remap, and math operations extended for Unity vectors or collections.

### RandomExtensions
Methods that extend Unity.Random, i.e. getting a point on unit circle.

### SamplingExtensions
Sample multiple random items from a collection.

### UnityExtensions
Generally useful Unity extensions, such as checking for a Layer in LayerMask, getting perpendicular vector in 2D plane, 
or rotating transform to a given direction in 2D plane.

## Modifiers
Additive and multiplicative modifiers, that can support multiple sources contributing to that modifier.

`ValueModifier` supports only adding and removing a multiplier, and need to be carefully managed in code.

`WrappedValueModifier` returns a wrapper, that can be used to keep track af a multiplier, change it, and remove it when needed.

## NestedScriptableObjects
`ScriptableObjectReference` is a serialized class that can be use to reference ScriptableObject in a separate asset, as well as create and reference a nested ScriptableObject.

`NestedScriptableObject` is a ScriptableObject with another nested ScriptableObject. It kinda exists only to showcase its custom editor.

## Observer
An implementation of **observer** pattern, where every callback will be executed only a set number of times.

## Tween
A barebones tweening library. Supports tweening color, position and scale, as well as composing multiple tweens together.

## UIExtensions
Some useful extension components for `Unity.UI.Slider`

### SliderIntBar
Draws a second slider fill image, that fills only to `value` rounded down to integer.

### SliderAfterimage
Draws a second slider fill image, that gradually catches up to `value`.

### SliderMinMaxWindow
Draws an image that fills from `value - delta` to `value`.

## Editor
Property drawers and custom editors.

**Note** that some of them showcase how you can create a property drawers or custom editors for generic types, and use `dynamic` invoke to call methods with correct type parameters.

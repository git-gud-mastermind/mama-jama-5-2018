using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextTweening : MonoBehaviour {

	[SerializeField] float fadeInTime = 0.5f, fadeOutTime = 0.3f;

	TMPro.TextMeshProUGUI textUI;
	Sequence currentSequence;

	/// <summary>
	/// Directly change the text. Complete with fading in and out.
	/// </summary>
	/// <param name="text">The string to display in the UI</param>
	public void SetText(string text)
	{
		ResetSequence()
			.AppendCallback(() => textUI.text = text)
			.Append(textUI.DOFade(1, fadeInTime))
			.OnKill(() => currentSequence = null);	//Clear the in-class ref so we don't throw logs in console
	}

	/// <summary>
	/// Animate the text through a series of strings. Ends on last string.
	/// </summary>
	/// <param name="texts">The strings, in order, to display in the UI</param>
	/// <param name="sequenceDelay">Time between each string, unfaded</param>
	public void SetTextInSequence(List<string> texts, float sequenceDelay)
	{
		ResetSequence();

		PopulateCurrentSequence(texts, sequenceDelay);

		//As with basic SetText, we need to clear the reference in-class when we're done
		currentSequence.OnKill(() => currentSequence = null);
	}

	/// <summary>
	/// Animate the text through a series of strings. Fades in and out between each loop. (Loops infinitely)
	/// </summary>
	/// <param name="texts">The strings, in order, to display in the UI</param>
	/// <param name="loopDelay">Time between each string, unfaded</param>
	public void SetTextOnLoop(List<string> texts, float loopDelay)
	{
		//This is our loop version, so be sure to let it know to loop
		ResetSequence().SetLoops(-1);

		PopulateCurrentSequence(texts, loopDelay);
	}

	/// <summary>
	/// Adds the strings in order, with the delay between them
	/// </summary>
	/// <param name="texts"></param>
	/// <param name="delay"></param>
	void PopulateCurrentSequence(List<string> texts, float delay)
	{
		foreach (var item in texts)
		{
			currentSequence.AppendCallback(() => textUI.text = item);

			if (texts.IndexOf(item) == 0)
				currentSequence.Append(textUI.DOFade(1, fadeInTime));

			currentSequence.AppendInterval(delay);
		}
	}

	/// <summary>
	/// Kills any old sequence this tween controller has made and preps a new one (by fading out last text) 
	/// </summary>
	/// <returns>The fresh version of currentSequence</returns>
	Sequence ResetSequence()
	{
		if (currentSequence != null && currentSequence.IsPlaying())
		{
			currentSequence.Kill();
		}

		currentSequence = DOTween.Sequence()
			.Append(textUI.DOFade(0, fadeOutTime));

		return currentSequence;
	}

	private void Awake()
	{
		textUI = GetComponent<TMPro.TextMeshProUGUI>();
	}
}

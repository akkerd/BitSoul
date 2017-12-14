(
~clampedmodes = { |basefreq, env|

	var freqs, amps;

	freqs = [1, 6.267, 17.55, 34.39];
	amps  = [0.5, 0.25, 0.125, 0.06125];

	Klank.ar(`[freqs, amps, 0.2], env, basefreq);
};

{~clampedmodes.(100, Impulse.ar(10))}.play()
)

(
~freemodes = { |input, basefreq=100, res=80|
	var filtfreqs;

	// The actual filter freqs take these harmonic relationships:
	filtfreqs = basefreq * [1, 2.7565, 5.40392, 8.93295, 13.3443, 18.6379];

	BPF.ar(input, filtfreqs, 1/res).sum * 10
};

{~freemodes.(LFSaw.ar(4))}.play()
)

~rulerwave = Env([1, 0, -0.7, 0, 1], [0.3, 0.1, 0.1, 0.3], [4, -4, 4, -4]).asSignal(512).asWavetable;
~rulerwave.plot;
// Here let's plot it running at a frequency that speeds up.
// This approximates the actual trajectory of motion of the end of the ruler:
{Osc.kr(~rulerwave.as(LocalBuf), XLine.kr(50, 100, 1), mul: XLine.kr(1, 0.001, 1))}.plot(1)

// Now, every time the wave passes zero in a downwards-going direction, that represents the ruler thwacking on the table and therefore transmitting energy into the resonances.
// This code builds on the previous one to derive the thwacks - one at each downward zero crossing, with an energy proportional to the speed (==derivative of position, found using Slope)
(
{
	var motion, thwacks, isDown;
	motion = Osc.ar(~rulerwave.as(LocalBuf), XLine.kr(50, 100, 1), mul: XLine.kr(1, 0.001, 1));
	isDown = motion < 0;
	thwacks = Trig1.ar(isDown, 0) * (0-Slope.ar(motion)) * 0.01;
	thwacks = LPF.ar(thwacks, 500);
	[motion, isDown, thwacks]
}.plot(1)
)

(
{
	var motion, thwacks, isDown, basefreq;
	motion = Osc.ar(~rulerwave.as(LocalBuf), XLine.kr(10, 100, 1), mul: Line.kr(1, 0.001, 1, doneAction: 2));
	isDown = motion < 0;
	thwacks = Trig1.ar(isDown, 0) * (0-Slope.ar(motion)) * 0.01;
	thwacks = LPF.ar(thwacks, 500);

	basefreq = if(isDown, 289, 111);
	~freemodes.value(thwacks, basefreq, 100)
		+
	~clampedmodes.value(basefreq, thwacks);
}.play
)

// That was a model of a ruler-on-a-desk. The next one is... something else.

(
{
	var motion, thwacks, isDown, basefreq;
	motion = Osc.ar(~rulerwave.as(LocalBuf), 80, mul: Line.kr(1, 0.001, 1, doneAction: 2));
	isDown = motion < 0;
	thwacks = Trig1.ar(isDown, 0) * (0-Slope.ar(motion)) * 0.01;

	basefreq = if(isDown, 289, 111) * Pulse.ar(10).exprange(0.9, 1.1);
	~freemodes.value(thwacks, basefreq, 100)
		+
	~clampedmodes.value(basefreq, thwacks);
}.play
)

(
Ndef(\bouncer, {

var bounceperiod, bouncetrigs, amp, fm, mainosc;

	bounceperiod = Line.kr(0.1, 0, 1, doneAction: 2);

bouncetrigs = Impulse.kr(bounceperiod.reciprocal.min(30));

amp = EnvGen.ar(Env.perc(0.001, 0.0), bouncetrigs);
amp = Amplitude.ar(amp, 0, bounceperiod) * Line.kr(1, 0.05, 3);

fm =
	SinOsc.ar(120).range(0, Line.ar(1, 0, 3))
		+
	(amp * Line.ar(1, 0, 3).cubed * 130 + 80)
;

mainosc = SinOsc.ar(fm, pi/2);

amp * mainosc;
}).play
)
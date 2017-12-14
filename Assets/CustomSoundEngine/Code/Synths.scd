s.boot;


a = {
	arg noiseHz=10;
	var freq, amp, sig;
	freq = LFNoise0.kr(noiseHz).exprange(700, 200);
	amp = LFNoise1.kr(12).exprange(0.02,1);
	sig = SinOsc.ar(freq) * amp;
}.play;
)

(
SynthDef.new(\sine, {
	arg ampHz=4, fund=40, maxPartial=4;
	var freq1, freq2, amp1, amp2, sig1, sig2;
	freq1 = LFNoise0.kr(4).exprange(fund, fund*maxPartial).round(fund);
	freq2 = LFNoise0.kr(4).exprange(fund, fund*maxPartial).round(fund);
	freq1 = freq1 * LFPulse.kr(8, add:1);
	freq1 = freq1 * LFPulse.kr(6, add:1);
	amp1 = LFPulse.kr(ampHz, 0, 0.12);
	amp2 = LFPulse.kr(ampHz, 0.5, 0.12);
	sig1 = Pulse.ar(freq1,0.5, amp1);
	sig2 = Pulse.ar(freq2, 0.5, amp2);
	sig1 = FreeVerb.ar(sig1, 0.7, 0.8, 0.25);
	sig2 = FreeVerb.ar(sig2, 0.7, 0.8, 0.25);
	Out.ar(0,sig1);
	Out.ar(1,sig2);
}).add;
)

Synth.new(\sine, [\ampHz, 5]);


(
play({
	z = Blip.ar(800, 6, 0.1);
	PitchShift.ar(z, 0.02, Line.kr(0.1,4,20), 0, 0.0001)
}))


s.boot;

(
z = {
	arg freq=340, amp=1;
	var sig;
	sig = SinOsc.ar(freq) * amp;
}.play;
)

z.set(\freq, 330);
z.set(\freq, 530, \amp, 1);
z.set(\freq, 530, \amp, 0.1);

(
a = {
	arg noiseHz=10;
	var freq, amp, sig;
	freq = LFNoise0.kr(noiseHz).exprange(700, 200);
	amp = LFNoise1.kr(12).exprange(0.02,1);
	sig = SinOsc.ar(freq) * amp;
}.play;
)

z.set(\noiseHz, 5);
z.set(\noiseHz, 4);
z.set(\noiseHz, 0);
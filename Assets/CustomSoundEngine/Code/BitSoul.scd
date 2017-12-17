(
s.reboot;

~filePath = "C:/Users/ANIA/Documents/GitHub/BitSoul/Assets/CustomSoundEngine/Samples/";

~buffArrayIdle = [
	"hello.wav",
	"hey.wav",
	"look.wav",
	"listen.wav"
].collect({ arg snd; Buffer.read(Server.default, ~filePath++snd)});

// NOTE DEFS ///////////////////////////
~dur = PatternProxy(Pn(0.25, inf));
~repeat = PatternProxy(Pn(2, 4));

m = Pdef(\gmajor7, Pbind(\dur, ~dur, \freq, Pseq( [67,71,62,67,71,62,67,71,66].midicps)));
n = Pdef(\fmajor7, Pbind(\dur, ~dur, \freq, Pseq( [65,69,60,65,69,60,65,69,64].midicps)));
o = Pdef(\fmajor7, Pbind(\dur, ~dur, \freq, Pseq( [63,66,71,63,66,71,63,66,62].midicps)));

c = Pdef(\cmajor, Pbind(\dur, ~dur, \freq, Pseq( [60,64,67].midicps ,~repeat)));
f = Pdef(\fmajor, Pbind(\dur, ~dur, \freq, Pseq( [65,69,60].midicps ,~repeat)));
g = Pdef(\gmajor, Pbind(\dur, ~dur, \freq, Pseq( [67,71,62].midicps ,~repeat)));
e = Pdef(\eminor, Pbind(\dur, ~dur, \freq, Pseq( [64,67,71].midicps ,~repeat)));
a = Pdef(\aminor, Pbind(\dur, ~dur, \freq, Pseq( [69,60,64].midicps ,~repeat)));
d = Pdef(\dminor, Pbind(\dur, ~dur, \freq, Pseq( [62,65,69].midicps ,~repeat)));
b = Pdef(\bmajor, Pbind(\dur, ~dur, \freq, Pseq( [59,63,66].midicps ,~repeat)));

/*c = Pdef(\cmajor, Pbind(\dur, ~dur, \freq, Pxrand( [60,64,67].midicps )));
f = Pdef(\fmajor, Pbind(\dur, ~dur, \freq, Pxrand( [65,69,60].midicps )));
g = Pdef(\gmajor, Pbind(\dur, ~dur, \freq, Pxrand( [67,71,62].midicps )));
e = Pdef(\eminor, Pbind(\dur, ~dur, \freq, Pxrand( [64,67,71].midicps )));
a = Pdef(\aminor, Pbind(\dur, ~dur, \freq, Pxrand( [69,60,64].midicps )));
d = Pdef(\dminor, Pbind(\dur, ~dur, \freq, Pxrand( [62,65,69].midicps )));
b = Pdef(\bmajor, Pbind(\dur, ~dur, \freq, Pxrand( [59,63,66].midicps )));*/

//SYNTH DEFS/////////////

//SoundFX Synth

SynthDef(\sampler, {
	arg out = 0, rate = 1, amp = 1, buffer = 0;
	var signal;

	signal = PlayBuf.ar(2, buffer, BufRateScale.kr(buffer)*rate, doneAction:2
	);

	Out.ar(out, signal * amp);
}).add;

SynthDef.new(\soundtrack, {
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


SynthDef(\dsaf_multialarm, {
	|length=0.05, freqs=#[600,800,600,800], timbre=1, repeats=inf|
	var freq, out, operations;
	freq = Duty.ar(length, 0, Dseq(freqs, repeats), doneAction: 2);
	freq = LPF.ar(freq, 70);
	out = LeakDC.ar(SinOsc.ar(freq));
	out = Select.ar(timbre, [out, (out * pi).sin, (out * pi).cos, ((out+0.25) * pi).cos]);
	// NOTE: when writing a synthdef always remember the Out ugen!
	// (Handy shortcuts like Ndef and {}.play often add Out on your behalf)
	Out.ar(0, Pan2.ar(out * 0.1))
}).add;


OSCdef.new(
	\start,
	{arg msg;
		~dur.source = Pn(0.2,inf);
		~repeat.source = Pn(2, 4);
		~s1 = Pseq([c,f,c,f,e,c,f,o], inf).play;
		~s2 = Pseq([c,f,c,f,e,c,f,o], inf).play;
	},
	'/start',
	nil, 57120
);

OSCdef.new(
	\player,
	{arg msg;
		(
			// STOP all the previous sounds
			~s1.stop;
			~s2.stop;
			~x1.stop;
			~x2.stop;
			~y1.stop;
			~y2.stop;
			~z1.stop;
			~z2.stop;
			switch (msg.at(1).postln,
				1, { ~dur.source = Pn(0.2,inf);  ~repeat.source = Pn(2, 4); ~s1 = Pseq([c,f,c,f,e,c,f,o], inf).play; ~s2 = Pseq([c,f,c,f,e,c,f,o], inf).play; },
				3, { ~dur.source = Pn(0.17,inf);  ~repeat.source = Pn(1, 2); ~z1 = Pseq([g,b,c,f,m,c,e,f], inf).play; ~z2 = Pseq([g,b,c,f,m,c,e,f], inf).play;},
				5, { ~dur.source = Pn(0.8, inf); ~repeat.source = Pn(2, 2); ~y1 = Pseq([d,a,c,g,d,c,a,m], inf).play; ~y2 = Pseq([d,a,c,g,d,c,a,m], inf).play;},
				7, {  ~dur.source = Pn(0.125,inf); ~repeat.source = Pn(2, 4); ~x1 = Pseq([a,c,g,f,a,c,g,n], 2).play; ~x2 = Pseq([a,c,g,f,a,c,g,n], inf).play;},
			);
		)
	},
	'/player',
	nil, 57120
);

OSCdef.new(
	\wrongPlayer,
	{arg msg;
		// error code
		Synth(\dsaf_multialarm, [\length, 0.003, \freqs, [ rrand(800, 1000), 0,  rrand(800, 1000), 0], \timbre, rrand(1, 3), \repeats, 30]);
	},
	'/wrongPlayer',
	nil, 57120
);

OSCdef.new(
	\stop,
	{
		x.free;
		~x1.stop;
		~x2.stop;
		~y1.stop;
		~y2.stop;
		~z1.stop;
		~z2.stop;
	},
	'/stop',
	nil, 57120
);

OSCdef.new(
	\sampler,
	{
		arg msg;
		Synth.new(\sampler, [\buffer, Buffer.read(Server.default,~filePath++msg.at(1))]);
	},
	'/sampler',
	nil, 57120
);
)

x = Synth.new(\soundtrack);